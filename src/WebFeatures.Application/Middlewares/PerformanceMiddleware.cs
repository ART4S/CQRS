using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Requests;
using WebFeatures.Application.Interfaces.Logging;

namespace WebFeatures.Application.Middlewares
{
    /// <summary>
    /// Logs long running requests
    /// </summary>
    internal class PerformanceMiddleware<TRequest, TResponse> : IRequestMiddleware<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request, RequestDelegate<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();

            sw.Start();

            TResponse response = await next();

            sw.Stop();

            if (sw.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Long running request: {typeof(TRequest).Name} = {sw.ElapsedMilliseconds} ms");
            }

            return response;
        }
    }
}