﻿using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using WebFeatures.Application.Constants;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.Products.Requests.Commands
{
    /// <summary>
    /// Редактировать товар
    /// </summary>
    public class UpdateProduct : ICommand<Empty>, IHasMappings
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
            profile.CreateMap<UpdateProduct, Product>(MemberList.Source)
                .ForMember(dest => dest.MainPicture, opt => opt.Ignore())
                .ForSourceMember(src => src.Pictures, opt => opt.DoNotValidate());
        }

        public class Validator : AbstractValidator<UpdateProduct>
        {
            public Validator(IWriteDbContext db)
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
                        System.IO.Path.GetExtension(x.Name)))
                    .WithMessage(ValidationConstants.Products.PictureFormatError)
                    .When(x => x.MainPicture != null);

                RuleFor(x => x.Pictures)
                    .Must(x => x.All(y => ValidationConstants.Products.AllowedPictureFormats.Contains(
                        System.IO.Path.GetExtension(y.Name))))
                    .WithMessage(ValidationConstants.Products.PictureFormatError);
            }
        }
    }
}