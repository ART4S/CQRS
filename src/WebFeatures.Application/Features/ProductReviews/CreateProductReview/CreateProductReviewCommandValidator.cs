using FluentValidation;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.ProductReviews.CreateProductReview
{
    public class CreateProductReviewCommandValidator : AbstractValidator<CreateProductReviewCommand>
    {
        public CreateProductReviewCommandValidator(IWriteDbContext db)
        {
            RuleFor(x => x.ProductId).MustAsync((x, t) => db.Products.ExistsAsync(x));
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty();
        }
    }
}