using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductReviews
{
    internal class GetProductReviewsQueryHandler : IRequestHandler<GetProductReviewsQuery, IEnumerable<ProductReviewInfoDto>>
    {
        private readonly IReadDbContext _db;

        public GetProductReviewsQueryHandler(IReadDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<ProductReviewInfoDto>> HandleAsync(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            return _db.Products.GetReviewsAsync(request.Id);
        }
    }
}