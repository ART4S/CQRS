using System;
using WebFeatures.Domian.Enums;

namespace WebFeatures.Application.Features.Products.GetProductReviews
{
    /// <summary>
    /// Инофрмация оо обзоре на товар
    /// </summary>
    public class ProductReviewInfoDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Пользовательская оценка
        /// </summary>
        public ProductRating Rating { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Имя автора
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }
    }
}