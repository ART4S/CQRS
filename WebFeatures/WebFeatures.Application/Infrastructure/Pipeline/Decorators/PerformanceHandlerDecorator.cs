using System.Diagnostics;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Infrastructure.Pipeline.Decorators
{
    /// <summary>
    /// Логирование долгих запросов
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

        public override TResponse Handle(TRequest request)
        {
            _timer.Start();
            var result = Decoratee.Handle(request);
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                _logger.LogWarning($"Долгий запрос: {typeof(TRequest).Name} = {_timer.ElapsedMilliseconds} milliseconds");
            }

            return result;
        }
    }
}
