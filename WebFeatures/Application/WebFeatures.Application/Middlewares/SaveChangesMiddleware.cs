using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    internal class SaveChangesMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IWriteContext _db;
        private readonly ILogger<TRequest> _logger;

        public SaveChangesMiddleware(IWriteContext db, ILogger<TRequest> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();
            await _db.SaveChangesAsync();
            return response;
        }
    }
}
