using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    public class GetProductsListHandler : IRequestHandler<GetProductsList, IQueryable<ProductListDto>>
    {
        private readonly IAsyncRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public GetProductsListHandler(
            IAsyncRepository<Product> productsRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        public Task<IQueryable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            var products = _productsRepo.GetAll()
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider);

            return Task.FromResult(products);
        }
    }
}
