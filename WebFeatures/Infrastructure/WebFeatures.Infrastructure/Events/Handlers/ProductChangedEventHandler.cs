using Dapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Infrastructure.DataAccess.Constants;

namespace WebFeatures.Infrastructure.EventHandlers
{
    internal class ProductChangedEventHandler :
        IEventHandler<ProductCreated>,
        IEventHandler<ProductUpdated>
    {
        private readonly ILogger<ProductChangedEventHandler> _logger;
        private readonly IWriteDbContext _db;

        public ProductChangedEventHandler(
            ILogger<ProductChangedEventHandler> logger,
            IWriteDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public Task HandleAsync(ProductCreated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        public Task HandleAsync(ProductUpdated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        private async Task RefreshViewsAsync()
        {
            string sql = $"REFRESH MATERIALIZED VIEW CONCURRENTLY {ViewNames.GET_PRODUCTS_LIST};";

            try
            {
                await _db.Connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update view '{ViewNames.GET_PRODUCTS_LIST}'", ex);
            }
        }
    }
}