using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.Requests.EditProduct
{
    public class EditProduct : ICommand<Empty>, IHasMappings
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid BrandId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<EditProduct, Product>(MemberList.Source);
        }

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
            }
        }
    }
}
