using System.Linq;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.GetProductsList
{
    public class GetProductsList : IQuery<IQueryable<ProductListDto>>
    {
    }
}