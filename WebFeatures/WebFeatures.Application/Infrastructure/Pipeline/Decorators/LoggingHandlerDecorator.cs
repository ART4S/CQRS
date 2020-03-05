using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Infrastructure.Pipeline.Decorators
{
    /// <summary>
    /// Request logging
    /// </summary>
    public class LoggingHandlerDecorator<TRequest, TResponse> : RequestHandlerDecoratorBase<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingHandlerDecorator(
            IRequestHandler<TRequest, TResponse> decoratee,
            ILogger<TRequest> logger) : base(decoratee)
        {
            _logger = logger;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            var response = await Decoratee.HandleAsync(request);
            _logger.LogInformation($"{Decoratee.GetType().Name} : {request.ToString()} => {response.ToString()}");
            return response;
        }
    }
}
