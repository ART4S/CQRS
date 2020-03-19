using System;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Request logging
    /// </summary>
    internal class LoggingMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var response = await next(request);
            _logger.LogInformation($"{request.ToString()} => {response.ToString()}");
            return response;
        }
    }
}
