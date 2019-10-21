namespace WebFeatures.Application.Infrastructure.Pipeline.Abstractions
{
    /// <summary>
    /// Декоратор для обработчика
    /// </summary>
    public abstract class HandlerDecoratorBase<TIn, TOut> : IHandler<TIn, TOut>
    {
        protected readonly IHandler<TIn, TOut> Decoratee;

        protected HandlerDecoratorBase(IHandler<TIn, TOut> decoratee)
        {
            Decoratee = decoratee;
        }

        public abstract TOut Handle(TIn input);
    }
}
