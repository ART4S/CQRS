using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Requests.Commands;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.Handlers
{
    internal class ProductCommandHandler :
        IRequestHandler<CreateProduct, Guid>,
        IRequestHandler<UpdateProduct, Empty>
    {
        private readonly IWriteDbContext _db;
        private readonly IMapper _mapper;

        public ProductCommandHandler(IWriteDbContext db, IMapper mapper)
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

        public async Task<Empty> HandleAsync(UpdateProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.GetAsync(request.Id);

            _mapper.Map(request, product);

            await _db.Products.UpdateAsync(product);

            return Empty.Value;
        }
    }
}