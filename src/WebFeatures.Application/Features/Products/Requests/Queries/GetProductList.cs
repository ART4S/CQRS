using System.Collections.Generic;
using WebFeatures.Application.Features.Products.Dto;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.Requests.Queries
{
    /// <summary>
    /// Получить список товаров
    /// </summary>
    public class GetProductList : IQuery<IEnumerable<ProductListDto>>
    {
    }
}