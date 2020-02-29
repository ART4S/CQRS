namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Декоратор для обработчика
    /// </summary>
    public abstract class RequestHandlerDecoratorBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        protected readonly IRequestHandler<TRequest, TResponse> Decoratee;

        protected RequestHandlerDecoratorBase(IRequestHandler<TRequest, TResponse> decoratee)
        {
            Decoratee = decoratee;
        }

        public abstract TResponse Handle(TRequest request);
    }
}
