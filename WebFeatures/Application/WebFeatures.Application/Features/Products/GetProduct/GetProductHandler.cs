using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProduct
{
    internal class GetProductHandler : IRequestHandler<GetProduct, ProductInfoDto>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductInfoDto> HandleAsync(GetProduct request, CancellationToken cancellationToken)
        {
            Product product = await _db.Products.GetAsync(request.Id);
            return _mapper.Map<ProductInfoDto>(product);
        }
    }
}
