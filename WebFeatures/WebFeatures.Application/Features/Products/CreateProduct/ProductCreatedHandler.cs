using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        public Task HandleAsync(ProductCreated eve, CancellationToken cancellationToken)
        {
            // TODO: update product through hangfire
            return Task.CompletedTask;
        }
    }
}
