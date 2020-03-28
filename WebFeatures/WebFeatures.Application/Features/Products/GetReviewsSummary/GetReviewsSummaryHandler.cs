using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;
using WebFeatures.Domian.Enums;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetReviewsSummary
{
    public class GetReviewsSummaryHandler : IRequestHandler<GetReviewsSummary, ReviewsSummaryDto>
    {
        private readonly IWebFeaturesDbContext _db;

        public GetReviewsSummaryHandler(IWebFeaturesDbContext db) => _db = db;

        public async Task<ReviewsSummaryDto> HandleAsync(GetReviewsSummary request, CancellationToken cancellationToken)
        {
            IQueryable<Review> reviews = _db.Reviews
                .Where(x => x.ProductId == request.ProductId);

            var summary = new ReviewsSummaryDto()
            {
                OneStarsCount = await reviews.CountAsync(x => x.Rating == UserRating.OneStar, cancellationToken),
                TwoStarsCount = await reviews.CountAsync(x => x.Rating == UserRating.TwoStars, cancellationToken),
                ThreeStarsCount = await reviews.CountAsync(x => x.Rating == UserRating.ThreeStars, cancellationToken),
                FourStarsCount = await reviews.CountAsync(x => x.Rating == UserRating.FourStars, cancellationToken),
                FiveStarsCount = await reviews.CountAsync(x => x.Rating == UserRating.FiveStars, cancellationToken),
                ReviewsCount = await reviews.CountAsync(cancellationToken),
                RatingAverage = await reviews.AverageAsync(x => (int)x.Rating, cancellationToken)
            };

            return summary;
        }
    }
}
