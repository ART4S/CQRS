using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    internal class CreateProductHandler : IRequestHandler<CreateProduct, Guid>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public CreateProductHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Guid> HandleAsync(CreateProduct request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            await _db.Products.CreateAsync(product);

            return product.Id;
        }
    }
}
