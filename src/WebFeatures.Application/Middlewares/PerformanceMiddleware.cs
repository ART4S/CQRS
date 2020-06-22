using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.Logging;
using WebFeatures.Application.Interfaces.Requests;

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

            const int maxAcceptableTime = 500;

            if (sw.ElapsedMilliseconds > maxAcceptableTime)
            {
                _logger.LogWarning($"Long running request: {typeof(TRequest).Name} = {sw.ElapsedMilliseconds} ms");
            }

            return response;
        }
    }
}