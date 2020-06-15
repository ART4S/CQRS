using System;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    /// <summary>
    /// Получить товар
    /// </summary>
    public class GetProduct : IQuery<ProductInfoDto>
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }
    }
}
