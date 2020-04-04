using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Events;
using WebFeatures.HangfireJobs;

namespace WebFeatures.Application.Features.Products.Requests.EditProduct
{
    public class ProductEditedEventHandler : IEventHandler<ProductEditedEvent>
    {
        private readonly IBackgroundJobManager _jobManager;

        public ProductEditedEventHandler(IBackgroundJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public Task HandleAsync(ProductEditedEvent eve, CancellationToken cancellationToken)
        {
            return _jobManager.EnqueueAsync(new UpsertProductJobArg(eve.ProductId));
        }
    }
}
