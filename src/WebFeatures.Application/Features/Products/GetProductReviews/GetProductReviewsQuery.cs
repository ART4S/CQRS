using System;
using System.Collections.Generic;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductReviews
{
    /// <summary>
    /// Получить обзоры на товар
    /// </summary>
    public class GetProductReviewsQuery : IQuery<IEnumerable<ProductReviewInfoDto>>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}
