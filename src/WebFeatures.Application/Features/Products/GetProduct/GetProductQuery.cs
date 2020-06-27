using System;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProduct
{
    /// <summary>
    /// Получить товар
    /// </summary>
    public class GetProductQuery : IQuery<ProductInfoDto>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}
