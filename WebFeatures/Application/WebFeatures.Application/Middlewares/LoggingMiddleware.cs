using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Request logging
    /// </summary>
    internal class LoggingMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();
            _logger.LogInformation($"{request} => {response}");
            return response;
        }
    }
}
