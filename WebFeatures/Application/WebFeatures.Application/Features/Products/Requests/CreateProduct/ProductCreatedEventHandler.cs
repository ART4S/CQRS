using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.CreateProduct;
using WebFeatures.Events;
using WebFeatures.HangfireJobs;

namespace WebFeatures.Application.Features.Products.Requests.CreateProduct
{
    public class ProductCreatedEventHandler : IEventHandler<ProductCreatedEvent>
    {
        private readonly IBackgroundJobManager _jobManager;

        public ProductCreatedEventHandler(IBackgroundJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public Task HandleAsync(ProductCreatedEvent eve, CancellationToken cancellationToken)
        {
            return _jobManager.EnqueueAsync(new UpsertProductJobArg(eve.ProductId));
        }
    }
}
