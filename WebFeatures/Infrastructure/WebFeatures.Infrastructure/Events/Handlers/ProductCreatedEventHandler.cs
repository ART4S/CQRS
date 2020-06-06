using Dapper;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Infrastructure.DataAccess.Constants;

namespace WebFeatures.Infrastructure.EventHandlers
{
    internal class ProductCreatedEventHandler : IEventHandler<ProductCreated>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IWriteDbContext _db;

        public ProductCreatedEventHandler(
            ILogger<ProductCreatedEventHandler> logger,
            IWriteDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public Task HandleAsync(ProductCreated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        private async Task RefreshViewsAsync()
        {
            var sb = new StringBuilder();

            foreach (string view in ViewNames.Products.All)
            {
                sb.AppendLine($"REFRESH MATERIALIZED VIEW CONCURRENTLY {view};");
            }

            try
            {
                await _db.Connection.ExecuteAsync(sb.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot update product's views", ex);
            }
        }
    }
}