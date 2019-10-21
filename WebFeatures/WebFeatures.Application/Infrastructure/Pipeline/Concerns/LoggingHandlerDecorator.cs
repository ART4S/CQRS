using Microsoft.Extensions.Logging;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Infrastructure.Pipeline.Concerns
{
    /// <summary>
    /// Логирование запросов
    /// </summary>
    public class LoggingHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly ILogger<TIn> _logger;

        public LoggingHandlerDecorator(IHandler<TIn, TOut> decorated, ILogger<TIn> logger) : base(decorated)
        {
            _logger = logger;
        }

        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _logger.LogInformation($"{Decoratee.GetType().Name} : {input.ToString()} => {result.ToString()}");
            return result;
        }
    }
}
