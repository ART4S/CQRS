using Dapper;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Features.ProductComments.Events;
using WebFeatures.Application.Features.ProductReviews.Events;
using WebFeatures.Application.Features.Products.Events;
using WebFeatures.Application.Infrastructure.Events;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Infrastructure.DataAccess.Constants;

namespace WebFeatures.Infrastructure.EventHandlers
{
    internal class ProductChangedEventHandler :
        IEventHandler<ProductCreated>,
        IEventHandler<ProductUpdated>,
        IEventHandler<ProductCommentCreated>,
        IEventHandler<ProductReviewCreated>
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
            return RefreshAllViewsAsync();
        }

        public Task HandleAsync(ProductUpdated eve, CancellationToken cancellationToken)
        {
            return RefreshAllViewsAsync();
        }

        public Task HandleAsync(ProductCommentCreated eve, CancellationToken cancellationToken)
        {
            return RefreshViewsAsync();
        }

        public Task HandleAsync(ProductReviewCreated eve, CancellationToken cancellationToken)
        {
            return RefreshReviewViewsAsync();
        }

        private async Task RefreshViewsAsync()
        {
            string sql = $"REFRESH MATERIALIZED VIEW CONCURRENTLY {ViewNames.Products.GET_PRODUCT_COMMENTS};";

            try
            {
                await _db.Connection.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update view '{ViewNames.Products.GET_PRODUCT_COMMENTS}'", ex);
            }
        }

        private async Task RefreshAllViewsAsync()
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

        private async Task RefreshReviewViewsAsync()
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