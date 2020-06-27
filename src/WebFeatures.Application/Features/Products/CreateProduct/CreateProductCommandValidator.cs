using FluentValidation;
using System.Linq;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IWriteDbContext db)
        {
            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.Price)
                .Must(x => x >= 0)
                .When(x => x.Price.HasValue);

            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ManufacturerId).MustAsync((x, t) => db.Manufacturers.ExistsAsync(x));

            RuleFor(x => x.CategoryId)
                .MustAsync((x, t) => db.Categories.ExistsAsync(x.Value))
                .When(x => x.CategoryId.HasValue);

            RuleFor(x => x.BrandId).MustAsync((x, t) => db.Brands.ExistsAsync(x));

            RuleFor(x => x.MainPicture)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(x => ValidationConstants.Products.AllowedPictureFormats.Contains(
                    System.IO.Path.GetExtension(x.Name)))
                .WithMessage(ValidationConstants.Products.PictureFormatError);

            RuleFor(x => x.Pictures)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(x => x.All(y => ValidationConstants.Products.AllowedPictureFormats.Contains(
                    System.IO.Path.GetExtension(y.Name))))
                .WithMessage(ValidationConstants.Products.PictureFormatError);
        }
    }
}