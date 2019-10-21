using System;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Application.Infrastructure.Pipeline.Mediators
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TOut Send<TOut>(ICommand<TOut> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TOut));
            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic input = command;

            return handler.Handle(input);
        }

        public TOut Send<TOut>(IQuery<TOut> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TOut));
            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic input = query;

            return handler.Handle(input);
        }
    }
}
