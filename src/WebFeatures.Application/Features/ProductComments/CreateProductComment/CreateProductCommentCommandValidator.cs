using FluentValidation;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
{
	public class CreateProductCommentCommandValidator : AbstractValidator<CreateProductCommentCommand>
	{
		public CreateProductCommentCommandValidator(IWriteDbContext db)
		{
			RuleFor(x => x.ProductId).MustAsync((x, t) => db.Products.ExistsAsync(x));
			RuleFor(x => x.Body).NotEmpty();

			RuleFor(x => x.ParentCommentId)
			   .MustAsync((x, t) => db.ProductComments.ExistsAsync(x.Value))
			   .When(x => x.ParentCommentId.HasValue);
		}
	}
}
