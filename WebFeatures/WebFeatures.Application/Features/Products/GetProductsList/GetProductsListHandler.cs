using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    public class GetProductsListHandler : IRequestHandler<GetProductsList, IQueryable<ProductListDto>>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly IMapper _mapper;

        public GetProductsListHandler(
            IWebFeaturesDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<IQueryable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            var products = _db.Products.ProjectTo<ProductListDto>(_mapper.ConfigurationProvider);
            return Task.FromResult(products);
        }
    }
}
