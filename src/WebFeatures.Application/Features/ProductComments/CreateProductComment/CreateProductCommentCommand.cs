using AutoMapper;
using System;
using WebFeatures.Application.Infrastructure.Mappings;
using WebFeatures.Application.Interfaces.Requests;
using WebFeatures.Domian.Entities.Products;

namespace WebFeatures.Application.Features.ProductComments.CreateProductComment
{
    /// <summary>
    /// Создать комментарий к товару
    /// </summary>
    public class CreateProductCommentCommand : ICommand<Guid>, IHasMappings
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Индентификатор родительского комментария
        /// </summary>
        public Guid? ParentCommentId { get; set; }

        public void ApplyMappings(Profile profile)
        {
            profile.CreateMap<CreateProductCommentCommand, ProductComment>(MemberList.Source);
        }
    }
}