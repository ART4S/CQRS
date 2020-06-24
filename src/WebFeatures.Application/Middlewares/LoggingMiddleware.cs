using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Logs request data
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
            Guid requestId = Guid.NewGuid();

            _logger.LogInformation("{0} started with params: {@Request}", typeof(TRequest).Name, request);

            TResponse response = await next();

            _logger.LogInformation("{0} finished with response: {@Response}", typeof(TRequest).Name, response);

            return response;
        }
    }
}