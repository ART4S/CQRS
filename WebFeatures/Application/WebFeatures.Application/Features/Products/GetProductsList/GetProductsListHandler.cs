using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    internal class GetProductsListHandler : IRequestHandler<GetProductsList, IQueryable<ProductListDto>>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductsListHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<IQueryable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            IQueryable<ProductListDto> products = _db.Products
                .AsQueryable()
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(products);
        }
    }
}
