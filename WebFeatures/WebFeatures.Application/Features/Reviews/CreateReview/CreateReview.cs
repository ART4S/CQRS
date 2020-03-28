using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReview : ICommand<Guid>
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public UserRating Rating { get; set; }

        public class Validator : AbstractValidator<CreateReview>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (x, token) => await db.Products.FindAsync(x) != null);
                RuleFor(x => x.Title)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
                RuleFor(x => x.Comment)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
            }
        }
    }
}
