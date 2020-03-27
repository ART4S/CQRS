using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductById
{
    internal class GetProductByIdHandler : IRequestHandler<GetProductById, ProductInfoDto>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IWebFeaturesDbContext db, IMapper mapper) 
            => (_db, _mapper) = (db, mapper);

        public async Task<ProductInfoDto> HandleAsync(GetProductById request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.FindAsync(request.Id);
            return _mapper.Map<ProductInfoDto>(product);
        }
    }
}
