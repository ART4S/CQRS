using System;
using System.Collections.Generic;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    /// <summary>
    /// Получить обзоры на товар
    /// </summary>
    public class GetProductReviews : IQuery<IEnumerable<ProductReviewInfoDto>>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}
