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
        private readonly Stopwatch _timer = new Stopwatch();

        public PerformanceHandlerDecorator(
            IRequestHandler<TRequest, TResponse> decoratee,
            ILogger<TRequest> logger) : base(decoratee)
        {
            _logger = logger;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            _timer.Start();
            var result = await Decoratee.HandleAsync(request);
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Долгий запрос: {typeof(TRequest).Name} = {_timer.ElapsedMilliseconds} milliseconds");
            }

            return result;
        }
    }
}
