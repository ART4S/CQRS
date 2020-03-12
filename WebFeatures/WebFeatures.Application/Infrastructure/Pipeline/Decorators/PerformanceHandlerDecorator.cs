using System.Diagnostics;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Infrastructure.Pipeline.Decorators
{
    /// <summary>
    /// Long running request logging
    /// </summary>
    public class PerformanceHandlerDecorator<TRequest, TResponse> : RequestHandlerDecoratorBase<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public PerformanceHandlerDecorator(
            IRequestHandler<TRequest, TResponse> decoratee,
            ILogger<TRequest> logger) : base(decoratee)
        {
            _logger = logger;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            var sw = new Stopwatch();

            sw.Start();
            var response = await Decoratee.HandleAsync(request);
            sw.Stop();

            if (sw.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Long running request: {typeof(TRequest).Name} = {sw.ElapsedMilliseconds} ms");
            }

            return response;
        }
    }
}
