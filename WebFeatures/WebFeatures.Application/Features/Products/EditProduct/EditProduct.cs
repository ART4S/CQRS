using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataContext;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class EditProduct : ICommand<Empty>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid? PictureId { get; set; }
        public byte[] Picture { get; set; }

        public class Validator : AbstractValidator<EditProduct>
        {
            public Validator(IWriteContext db)
            {
                RuleFor(x => x.Id)
                    .MustAsync(async (x, token) => await db.Products.FindAsync(x) != null);
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.ManufacturerId)
                    .MustAsync(async (x, token) => await db.Manufacturers.FindAsync(x) != null);
                RuleFor(x => x.CategoryId)
                    .MustAsync(async (x, token) => x == null || await db.Categories.FindAsync(x) != null);
                RuleFor(x => x.BrandId)
                    .MustAsync(async (x, token) => await db.Brands.FindAsync(x) != null);
                RuleFor(x => x.PictureId)
                    .MustAsync(async (x, token) => x == null || await db.Files.FindAsync(x) != null);
            }
        }
    }
}
