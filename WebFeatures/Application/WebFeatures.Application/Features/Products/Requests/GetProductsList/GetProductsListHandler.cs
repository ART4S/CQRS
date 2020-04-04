using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.Requests.GetProductsList
{
    public class GetProductsListHandler : IRequestHandler<GetProductsList, IQueryable<ProductListDto>>
    {
        private readonly IReadContext _db;
        private readonly IMapper _mapper;

        public GetProductsListHandler(IReadContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IQueryable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            IQueryable<ProductListDto> products =
                (await _db.GetAllAsync<Product>())
                .AsQueryable()
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider);

            return products;
        }
    }
}
