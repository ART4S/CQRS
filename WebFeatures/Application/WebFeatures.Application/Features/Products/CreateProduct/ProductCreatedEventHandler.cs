using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.CreateProduct
{
    internal class ProductCreatedEventHandler : IEventHandler<ProductCreatedEvent>
    {
        private readonly IBackgroundJobManager _jobManager;

        public ProductCreatedEventHandler(IBackgroundJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public Task HandleAsync(ProductCreatedEvent eve, CancellationToken cancellationToken)
        {
            return _jobManager.EnqueueAsync(new SyncEntityBetweenDatabasesJobArg<Product>(eve.ProductId));
        }
    }
}
