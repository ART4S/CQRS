using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductReviews.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Infrastructure.DataAccess.Constants;

namespace WebFeatures.Infrastructure.Events.Handlers
{
    internal class ProductReviewCreatedEventHandler : IEventHandler<ProductReviewCreated>
    {
        private readonly ILogger<ProductReviewCreatedEventHandler> _logger;
        private readonly IWriteDbContext _db;

        public ProductReviewCreatedEventHandler(
            ILogger<ProductReviewCreatedEventHandler> logger,
            IWriteDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public Task HandleAsync(ProductReviewCreated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        private async Task RefreshViewsAsync()
        {
            string sql = $"REFRESH MATERIALIZED VIEW CONCURRENTLY {ViewNames.Products.GET_PRODUCT_REVIEWS};";

            try
            {
                await _db.Connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update view '{ViewNames.Products.GET_PRODUCT_REVIEWS}'", ex);
            }
        }
    }
}