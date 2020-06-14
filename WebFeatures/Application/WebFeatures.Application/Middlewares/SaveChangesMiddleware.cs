using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Logging;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Applies all changes
    /// </summary>
    internal class SaveChangesMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IWriteDbContext _db;

        public SaveChangesMiddleware(ILogger<TRequest> logger, IWriteDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            try
            {
                await _db.SaveChangesAsync();

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving data", ex);
                throw;
            }
        }
    }
}