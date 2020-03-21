using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class SaveChangesMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IWebFeaturesDbContext _db;
        private readonly ILogger<TRequest> _logger;

        public SaveChangesMiddleware(IWebFeaturesDbContext db, ILogger<TRequest> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();
            await _db.SaveChangesAsync();
            return response;

            // TODO: uncomment when real database will be turned on

            //await using IDbContextTransaction transaction = await _db.BeginTransaction();

            //TResponse response = await next();

            //int retryCount = 0;

            //while (true)
            //{
            //    try
            //    {
            //        await _db.SaveChangesAsync();

            //        await transaction.CommitAsync(cancellationToken);

            //        return response;
            //    }
            //    catch (Exception ex)
            //    {
            //        await transaction.RollbackAsync(cancellationToken);

            //        retryCount++;

            //        if (retryCount == 5)
            //        {
            //            _logger.LogError("Error while performing database transaction", ex);
            //            throw;
            //        }
            //    }
            //}
        }
    }
}
