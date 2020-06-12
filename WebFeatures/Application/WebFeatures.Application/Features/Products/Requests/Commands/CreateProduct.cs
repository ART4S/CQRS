using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Domian.Entities.Products;

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
        /// Основное изображение
        /// </summary>
        public IFile MainPicture { get; set; }

        /// <summary>
        /// Изображения
        /// </summary>
        public IFile[] Pictures { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProduct, Product>(MemberList.Source)
                .ForMember(dest => dest.MainPicture, opt => opt.Ignore())
                .ForSourceMember(src => src.Pictures, opt => opt.DoNotValidate());
        }

        public class Validator : AbstractValidator<CreateProduct>
        {
            public Validator(IWriteDbContext db)
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Price).Must(price => price == null || price.Value >= 0);
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
                    .NotNull()
                    .Must(x => x.All(y => ValidationConstants.Products.AllowedPictureFormats.Contains(
                        System.IO.Path.GetExtension(y.Name))))
                    .WithMessage(ValidationConstants.Products.PictureFormatError);
            }
        }
    }
}