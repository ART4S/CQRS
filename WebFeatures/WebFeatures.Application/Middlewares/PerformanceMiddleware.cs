using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces;
using WebFeatures.Requests;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Long running request logging
    /// </summary>
    internal class PerformanceMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, Func<TRequest, Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();

            sw.Start();
            var response = await next(request);
            sw.Stop();

            if (sw.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Long running request: {typeof(TRequest).Name} = {sw.ElapsedMilliseconds} ms");
            }

            return response;
        }
    }
}
