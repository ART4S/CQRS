using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.Requests.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, Guid>
    {
        private readonly IWriteContext _db;
        private readonly IMapper _mapper;

        public CreateProductHandler(IWriteContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateProduct request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            await _db.Products.AddAsync(product, cancellationToken);

            product.Events.Add(new ProductCreatedEvent(product.Id));

            return product.Id;
        }
    }
}
