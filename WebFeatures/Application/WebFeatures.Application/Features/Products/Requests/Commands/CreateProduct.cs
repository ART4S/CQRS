using AutoMapper;
using FluentValidation;
using System;
using WebFeatures.Application.Infrastructure.Files;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Domian.Entities;

namespace WebFeatures.Application.Features.Products.Requests.Commands
{
    /// <summary>
    /// Создать товар
    /// </summary>
    public class CreateProduct : ICommand<Guid>, IHasMappings
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор производителя
        /// </summary>
        public Guid ManufacturerId { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Идентификатор бренда
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// Изображения
        /// </summary>
        public IFile[] Pictures { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProduct, Product>(MemberList.Source)
                .ForSourceMember(src => src.Pictures, opt => opt.DoNotValidate());
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

                RuleFor(p => p.Pictures)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .Must(p => p.Length != 0);
            }
        }
    }
}
