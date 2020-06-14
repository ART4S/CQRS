using AutoMapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.Products.Handlers
{
    internal class ProductCommandHandler :
        IRequestHandler<CreateProduct, Guid>,
        IRequestHandler<UpdateProduct, Empty>,
        IRequestHandler<DeleteProduct, Empty>
    {
        private readonly IWriteDbContext _db;
        private readonly IEventMediator _events;
        private readonly IFileReader _fileReader;
        private readonly IMapper _mapper;

        public ProductCommandHandler(
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

        public async Task<Guid> HandleAsync(CreateProduct request, CancellationToken cancellationToken)
        {
            var mainPicture = await _fileReader.ReadAsync(request.MainPicture, cancellationToken);

            await _db.Files.CreateAsync(mainPicture);

            Product product = _mapper.Map<Product>(request);

            product.MainPictureId = mainPicture.Id;

            await _db.Products.CreateAsync(product);

            foreach (IFile picture in request.Pictures)
            {
                var pictureFile = await _fileReader.ReadAsync(picture, cancellationToken);

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

        public async Task<Empty> HandleAsync(UpdateProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.GetAsync(request.Id);

            _mapper.Map(request, product);

            if (request.MainPicture != null)
            {
                var newPicture = await _fileReader.ReadAsync(request.MainPicture, cancellationToken);

                if (product.MainPictureId.HasValue)
                {
                    var oldPicture = await _db.Files.GetAsync(product.MainPictureId.Value);

                    if (!string.Equals(oldPicture.CheckSum, newPicture.CheckSum, StringComparison.OrdinalIgnoreCase))
                    {
                        await _db.Files.DeleteAsync(oldPicture);
                        await _db.Files.CreateAsync(newPicture);

                        product.MainPictureId = newPicture.Id;
                    }
                }
                else
                {
                    await _db.Files.CreateAsync(newPicture);

                    product.MainPictureId = newPicture.Id;
                }
            }

            await _db.Products.UpdateAsync(product);

            var actualPictures = await Task.WhenAll(
                request.Pictures.Select(x => _fileReader.ReadAsync(x, cancellationToken)));

            var oldPictures = (await _db.Files.GetByProductIdAsync(product.Id)).ToHashSet();

            var newPictures = actualPictures.Where(n => oldPictures.All(
                o => !string.Equals(n.CheckSum, o.CheckSum, StringComparison.OrdinalIgnoreCase)));

            foreach (File picture in newPictures)
            {
                await _db.Files.CreateAsync(picture);

                await _db.ProductPictures.CreateAsync(new ProductPicture()
                {
                    ProductId = product.Id,
                    FileId = picture.Id
                });
            }

            await _db.Files.DeleteAsync(oldPictures);

            await _events.PublishAsync(new ProductUpdated(product.Id));

            return Empty.Value;
        }

        public async Task<Empty> HandleAsync(DeleteProduct request, CancellationToken cancellationToken)
        {
            await _db.Products.DeleteAsync(request.Id);

            await _events.PublishAsync(new ProductDeleted());

            return Empty.Value;
        }
    }
}