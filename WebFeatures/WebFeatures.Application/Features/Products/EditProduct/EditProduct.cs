using FluentValidation;
using System;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class EditProduct : ICommand<Empty>
    {
        public Guid Id { get; set; }

        public Guid PictureId { get; set; }

        public ProductEditDto Product { get; set; }

        public class Validator : AbstractValidator<EditProduct>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.Product.Name).NotEmpty();
                RuleFor(x => x.Product.Description).NotEmpty();

                RuleFor(x => x.Product.ManufacturerId)
                    .MustAsync(async (x, token) => await db.Manufacturers.FindAsync(x) != null);

                RuleFor(x => x.Product.CategoryId)
                    .MustAsync(async (x, token) => !x.HasValue || await db.Categories.FindAsync(x) != null);

                RuleFor(x => x.Product.BrandId)
                    .MustAsync(async (x, token) => await db.Brands.FindAsync(x) != null);


                RuleFor(x => x.PictureId)
                    .MustAsync(async (x, token) => await db.Files.FindAsync(x) != null);
            }
        }
    }
}
