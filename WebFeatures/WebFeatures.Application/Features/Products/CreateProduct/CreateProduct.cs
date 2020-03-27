using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class CreateProduct : ICommand<Guid>
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public byte[] Picture { get; set; }

        public class Validator : AbstractValidator<CreateProduct>
        {
            public Validator(IWebFeaturesDbContext db)
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Price).Must(x => x == null || x.Value >= 0);
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.ManufacturerId)
                    .MustAsync(async (x, token) => await db.Manufacturers.FindAsync(x) != null);
                RuleFor(x => x.CategoryId)
                    .MustAsync(async (x, token) => x == null || await db.Categories.FindAsync(x) != null);
                RuleFor(x => x.BrandId)
                    .MustAsync(async (x, token) => await db.Brands.FindAsync(x) != null);
            }
        }
    }
}
