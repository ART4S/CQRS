using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Reviews.CreateReview
{
    public class CreateReview : ICommand<Empty>
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public UserRating Rating { get; set; }

        public class Validator : AbstractValidator<CreateReview>
        {
            public Validator(IWebFeaturesDbContext db, ICurrentUserService currentUser)
            {
                RuleFor(x => x.ProductId)
                    .MustAsync(async (productId, token) => await db.Products.FindAsync(productId) != null);

                RuleFor(x => x.ProductId).MustAsync(async (productId, token) => !await db.Reviews
                    .AnyAsync(x => x.ProductId == productId && x.AuthorId == currentUser.UserId))
                .WithMessage("User already reviewd this product");

                RuleFor(x => x.Title)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
                RuleFor(x => x.Comment)
                    .Must(x => !string.IsNullOrWhiteSpace(x));
            }
        }
    }
}
