using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Creates transaction
    /// </summary>
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
            await using DbTransaction transaction = await _db.Connection.BeginTransactionAsync();

            TResponse response = await next();

            int retryCount = 3;
            TimeSpan delay = TimeSpan.FromSeconds(5);

            while (true)
            {
                try
                {
                    await transaction.CommitAsync();

                    return response;
                }
                catch (Exception commitEx)
                {
                    if (--retryCount == 0)
                    {
                        _logger.LogError($"Error while comiting transaction", commitEx);

                        try
                        {
                            await transaction.RollbackAsync();
                        }
                        catch (Exception rollbackEx)
                        {
                            _logger.LogError($"Error while rolling back transaction", rollbackEx);

                            throw;
                        }

                        throw;
                    }
                }

                await Task.Delay(delay);
            }
        }
    }
}