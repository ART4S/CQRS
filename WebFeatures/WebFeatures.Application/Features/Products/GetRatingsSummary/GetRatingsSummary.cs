using System;
using FluentValidation;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.GetRatingsSummary
{
    public class GetRatingsSummary : IQuery<RatingsSummaryDto>
    {
        public Guid ProductId { get; set; }

        public class Validator : AbstractValidator<GetRatingsSummary>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, t) => await db.Products.FindAsync(x) != null);
            }
        }
    }
}
