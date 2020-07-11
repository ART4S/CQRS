using System.Collections.Generic;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Features.Products.GetProductList
{
	/// <summary>
	/// Получить список товаров
	/// </summary>
	public class GetProductListQuery : IQuery<IEnumerable<ProductListDto>>
	{
	}
}
