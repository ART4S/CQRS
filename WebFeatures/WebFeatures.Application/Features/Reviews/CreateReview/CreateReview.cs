using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReview : ICommand<Guid>
    {
        public Guid ProductId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }

        public class Validator : AbstractValidator<CreateReview>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, token) => await db.Products.FindAsync(x) != null);
                RuleFor(x => x.Header)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
                RuleFor(x => x.Body)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
            }
        }
    }
}
