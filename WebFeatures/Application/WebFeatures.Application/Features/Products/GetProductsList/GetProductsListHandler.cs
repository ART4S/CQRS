using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    internal class GetProductsListHandler : IRequestHandler<GetProductsList, IEnumerable<ProductListDto>>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductsListHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductListDto>> HandleAsync(GetProductsList request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductListDto> products = (await _db.Products.GetAllAsync())
                .Select(x => _mapper.Map<ProductListDto>(x));

            return products;
        }
    }
}
