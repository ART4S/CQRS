using System.Linq;
using WebFeatures.Application.Infrastructure.Requests;

namespace WebFeatures.Application.Features.Products.Requests.GetProductsList
{
    public class GetProductsList : IQuery<IQueryable<ProductListDto>>
    {
    }
}