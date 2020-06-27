using System;
using System.Collections.Generic;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductComments
{
    /// <summary>
    /// Получить комментарии к товару
    /// </summary>
    public class GetProductCommentsQuery : IQuery<IEnumerable<ProductCommentInfoDto>>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}