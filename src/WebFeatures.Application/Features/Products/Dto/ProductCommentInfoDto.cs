using System;

namespace WebFeatures.Application.Features.Products.Dto
{
    /// <summary>
    /// Информация о комментарии
    /// </summary>
    public class ProductCommentInfoDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string AuthorName { get; }

        /// <summary>
        /// Идентификатор родительского комментария
        /// </summary>
        public Guid? ParentCommentId { get; }
    }
}