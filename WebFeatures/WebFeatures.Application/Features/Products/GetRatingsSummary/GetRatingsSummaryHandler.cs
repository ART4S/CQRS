using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Enums;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetRatingsSummary
{
    public class GetRatingsSummaryHandler : IRequestHandler<GetRatingsSummary, RatingsSummaryDto>
    {
        private readonly IWebFeaturesDbContext _db;

        public GetRatingsSummaryHandler(IWebFeaturesDbContext db) => _db = db;

        public async Task<RatingsSummaryDto> HandleAsync(GetRatingsSummary request, CancellationToken cancellationToken)
        {
            List<UserRating> ratings = await _db.Reviews
                .Where(x => x.ProductId == request.ProductId)
                .Select(x => x.Rating)
                .ToListAsync(cancellationToken);

            var summary = new RatingsSummaryDto();

            int total = 0;

            foreach (UserRating rating in ratings)
            {
                total += (int)rating;

                summary.ReviewsCount++;

                switch (rating)
                {
                    case UserRating.OneStar:
                        summary.OneStarsCount++;
                        break;
                    case UserRating.TwoStars:
                        summary.TwoStarsCount++;
                        break;
                    case UserRating.ThreeStars:
                        summary.ThreeStarsCount++;
                        break;
                    case UserRating.FourStars:
                        summary.FourStarsCount++;
                        break;
                    case UserRating.FiveStars:
                        summary.FiveStarsCount++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(rating));
                }
            }

            summary.RatingAverage = total / summary.ReviewsCount;

            return summary;
        }
    }
}
