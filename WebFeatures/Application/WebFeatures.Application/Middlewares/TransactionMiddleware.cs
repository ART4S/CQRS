using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class TransactionMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IWriteDbContext _db;

        public TransactionMiddleware(ILogger<TRequest> logger, IWriteDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            IDbTransaction transaction = _db.BeginTransaction();
            TResponse response = await next();

            int retryCount = 3;
            TimeSpan delay = TimeSpan.FromSeconds(5);

            while (true)
            {
                try
                {
                    transaction.Commit();
                    return response;
                }
                catch (Exception ex)
                {
                    if (--retryCount == 0)
                    {
                        _logger.LogError("Transaction error", ex);
                        throw;
                    }
                }

                await Task.Delay(delay);
            }
        }
    }
}
