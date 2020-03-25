using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Requests;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    public class EditProductHandler : IRequestHandler<EditProduct, Empty>
    {
        public Task<Empty> HandleAsync(EditProduct request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
