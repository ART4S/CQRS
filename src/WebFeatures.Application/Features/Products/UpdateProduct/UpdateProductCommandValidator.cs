using System.IO;
using System.Linq;
using FluentValidation;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.Products.UpdateProduct
{
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator(IWriteDbContext db)
		{
			RuleFor(x => x.Id).MustAsync((x, t) => db.Products.ExistsAsync(x));
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Description).NotEmpty();
			RuleFor(x => x.ManufacturerId).MustAsync((x, t) => db.Manufacturers.ExistsAsync(x));

			RuleFor(x => x.CategoryId)
			   .MustAsync((x, t) => db.Categories.ExistsAsync(x.Value))
			   .When(x => x.CategoryId.HasValue);

			RuleFor(x => x.BrandId).MustAsync((x, t) => db.Brands.ExistsAsync(x));

			RuleFor(x => x.MainPicture)
			   .Must(x => ValidationConstants.Products.AllowedPictureFormats.Contains(
					Path.GetExtension(x.Name)))
			   .WithMessage(ValidationConstants.Products.PictureFormatError)
			   .When(x => x.MainPicture != null);

			RuleFor(x => x.Pictures)
			   .Cascade(CascadeMode.StopOnFirstFailure)
			   .NotNull()
			   .Must(x => x.All(y => ValidationConstants.Products.AllowedPictureFormats.Contains(
					Path.GetExtension(y.Name))))
			   .WithMessage(ValidationConstants.Products.PictureFormatError);
		}
	}
}
