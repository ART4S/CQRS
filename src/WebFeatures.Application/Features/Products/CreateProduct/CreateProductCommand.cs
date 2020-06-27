using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Interfaces.Files;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    /// <summary>
    /// Создать товар
    /// </summary>
    public class CreateProductCommand : ICommand<Guid>, IHasMappings
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
            profile.CreateMap<CreateProductCommand, Product>(MemberList.Source)
                .ForMember(dest => dest.MainPicture, opt => opt.Ignore())
                .ForSourceMember(src => src.Pictures, opt => opt.DoNotValidate());
        }
    }
}