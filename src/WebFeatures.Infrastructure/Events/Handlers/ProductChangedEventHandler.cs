using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Infrastructure.DataAccess.Constants;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.EventHandlers
{
    internal class ProductChangedEventHandler :
        IEventHandler<ProductCreated>,
        IEventHandler<ProductUpdated>,
        IEventHandler<ProductDeleted>
    {
        private readonly ILogger<ProductChangedEventHandler> _logger;
        private readonly IWriteDbContext _db;
        private readonly IDbExecutor _executor;

        public ProductChangedEventHandler(
            ILogger<ProductChangedEventHandler> logger,
            IWriteDbContext db,
            IDbExecutor executor)
        {
            _logger = logger;
            _db = db;
            _executor = executor;
        }

        public Task HandleAsync(ProductCreated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        public Task HandleAsync(ProductUpdated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        public Task HandleAsync(ProductDeleted eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        private async Task RefreshViewsAsync()
        {
            string sql = $"REFRESH MATERIALIZED VIEW CONCURRENTLY {ViewNames.GET_PRODUCTS_LIST};";

            try
            {
                await _executor.ExecuteAsync(_db.Connection, sql);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update view '{ViewNames.GET_PRODUCTS_LIST}'", ex);
            }
        }
    }
}