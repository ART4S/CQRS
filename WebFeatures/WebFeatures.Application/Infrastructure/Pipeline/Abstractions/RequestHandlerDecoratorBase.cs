using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Request handling decorator
    /// </summary>
    public abstract class RequestHandlerDecoratorBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        protected readonly IRequestHandler<TRequest, TResponse> Decoratee;

        protected RequestHandlerDecoratorBase(IRequestHandler<TRequest, TResponse> decoratee)
        {
            Decoratee = decoratee;
        }

        public abstract Task<TResponse> HandleAsync(TRequest request);
    }
}
