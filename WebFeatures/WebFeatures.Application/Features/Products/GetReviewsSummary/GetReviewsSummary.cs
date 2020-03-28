using System;
using FluentValidation;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.GetReviewsSummary
{
    public class GetReviewsSummary : IQuery<ReviewsSummaryDto>
    {
        public Guid ProductId { get; set; }

        public class Validator : AbstractValidator<GetReviewsSummary>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, token) => await db.Products.FindAsync(x) != null);
            }
        }
    }
}
