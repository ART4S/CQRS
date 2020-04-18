using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class CreateProduct : ICommand<Guid>, IHasMappings
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProduct, Product>(MemberList.Source);
        }

        public class Validator : AbstractValidator<CreateProduct>
        {
            public Validator(IDbContext db)
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Price).Must(x => x == null || x.Value >= 0);
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.ManufacturerId)
                    .MustAsync(async (x, t) => await db.Manufacturers.FindAsync(x) != null);
                RuleFor(x => x.CategoryId)
                    .MustAsync(async (x, t) => await db.Categories.FindAsync(x) != null)
                    .When(x => x.CategoryId.HasValue);
                RuleFor(x => x.BrandId)
                    .MustAsync(async (x, t) => await db.Brands.FindAsync(x) != null);
            }
        }
    }
}
