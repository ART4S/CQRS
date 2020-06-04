using Dapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Infrastructure.DataAccess.Constants;
using WebFeatures.Infrastructure.EventHandlers;

namespace WebFeatures.Infrastructure.Events.Handlers
{
    internal class ProductCommentCreatedEventHandler : IEventHandler<ProductCommentCreated>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IWriteDbContext _db;

        public ProductCommentCreatedEventHandler(
            ILogger<ProductCreatedEventHandler> logger,
            IWriteDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public Task HandleAsync(ProductCommentCreated eve, CancellationToken cancellationToken)
        {
            return UpdateRelatedMaterializedViewsAsync();
        }

        private async Task UpdateRelatedMaterializedViewsAsync()
        {
            string sql = $"REFRESH MATERIALIZED VIEW CONCURRENTLY {ViewNames.Products.GET_PRODUCT_COMMENTS};";

            try
            {
                await _db.Connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot update comment views", ex);
            }
        }
    }
}