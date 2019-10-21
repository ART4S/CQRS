using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;
using WebFeatures.Application.Interfaces;

namespace WebFeatures.Application.Infrastructure.Pipeline.Concerns
{
    /// <summary>
    /// Сохранение изменений контекста
    /// </summary>
    /// <remarks>Вызывается после работы всех декораторов для принятия изменений в рамках одной транзакции</remarks>
    public class SaveChangesHandlerDecorator<TIn, TOut> : HandlerDecoratorBase<TIn, TOut>
    {
        private readonly IAppContext _context;

        public SaveChangesHandlerDecorator(IHandler<TIn, TOut> decorated, IAppContext context) : base(decorated)
        {
            _context = context;
        }

        public override TOut Handle(TIn input)
        {
            var result = Decoratee.Handle(input);
            _context.SaveChanges();
            return result;
        }
    }
}
