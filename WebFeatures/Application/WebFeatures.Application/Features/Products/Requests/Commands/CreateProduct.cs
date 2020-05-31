using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.Requests.Commands
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
            public Validator(IWriteDbContext db)
            {
                RuleFor(p => p.Name)
                    .NotEmpty();

                RuleFor(p => p.Price)
                    .Must(price => price == null || price.Value >= 0);

                RuleFor(p => p.Description)
                    .NotEmpty();

                RuleFor(p => p.ManufacturerId)
                    .MustAsync(async (x, t) => await db.Manufacturers.ExistsAsync(x));

                RuleFor(p => p.CategoryId)
                    .MustAsync(async (x, t) => await db.Categories.ExistsAsync(x.Value))
                    .When(p => p.CategoryId.HasValue);

                RuleFor(p => p.BrandId)
                    .MustAsync(async (x, t) => await db.Brands.ExistsAsync(x));
            }
        }
    }
}
