using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    public class GetProductsList : IQuery<IEnumerable<ProductListDto>>
    {
    }
}