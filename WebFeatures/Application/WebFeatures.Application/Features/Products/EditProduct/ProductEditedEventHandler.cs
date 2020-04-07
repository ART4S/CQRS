using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Application.Jobs.Common;
using WebFeatures.Domian.Entities;
using WebFeatures.Events;

namespace WebFeatures.Application.Features.Products.EditProduct
{
    internal class ProductEditedEventHandler : IEventHandler<ProductEditedEvent>
    {
        private readonly IBackgroundJobManager _jobManager;

        public ProductEditedEventHandler(IBackgroundJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public Task HandleAsync(ProductEditedEvent eve, CancellationToken cancellationToken)
        {
            return _jobManager.EnqueueAsync(new SyncEntityBetweenDatabasesJobArg<Product>(eve.ProductId));
        }
    }
}
