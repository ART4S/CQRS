using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Events;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IWriteDbContext _db;
        private readonly IEventMediator _events;
        private readonly IFileReader _fileReader;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(
            IWriteDbContext db,
            IEventMediator events,
            IFileReader fileReader,
            IMapper mapper)
        {
            _db = db;
            _events = events;
            _fileReader = fileReader;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken)
        {
            File mainPicture = await _fileReader.ReadAsync(request.MainPicture, cancellationToken);

            await _db.Files.CreateAsync(mainPicture);

            Product product = _mapper.Map<Product>(request);

            product.MainPictureId = mainPicture.Id;

            await _db.Products.CreateAsync(product);

            foreach (IFile picture in request.Pictures)
            {
                File pictureFile = await _fileReader.ReadAsync(picture, cancellationToken);

                await _db.Files.CreateAsync(pictureFile);

                await _db.ProductPictures.CreateAsync(new ProductPicture()
                {
                    ProductId = product.Id,
                    FileId = pictureFile.Id
                });
            }

            await _events.PublishAsync(new ProductCreated(product.Id));

            return product.Id;
        }
    }
}