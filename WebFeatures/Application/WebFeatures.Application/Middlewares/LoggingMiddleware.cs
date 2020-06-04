using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.Logging;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Logs request
    /// </summary>
    internal class LoggingMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();

            _logger.LogInformation($"{request} => {response}");

            return response;
        }
    }
}
