using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductReviews
{
    internal class GetProductReviewsHandler : IRequestHandler<GetProductReviews, IEnumerable<ProductReviewInfoDto>>
    {
        private readonly IDbContext _db;
        private readonly IMapper _mapper;

        public GetProductReviewsHandler(IDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductReviewInfoDto>> HandleAsync(GetProductReviews request, CancellationToken cancellationToken)
        {
            IEnumerable<ProductReviewInfoDto> reviews =
                (await _db.ProductReviews.GetByProductAsync(request.ProductId))
                .Select(x => _mapper.Map<ProductReviewInfoDto>(x));

            return reviews;
        }
    }
}
