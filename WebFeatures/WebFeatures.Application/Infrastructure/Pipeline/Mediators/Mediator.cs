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

        public TResponse SendCommand<TResponse>(ICommand<TResponse> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(
                command.GetType(), 
                typeof(TResponse));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = command;

            return handler.Handle(request);
        }

        public TResponse SendQuery<TResponse>(IQuery<TResponse> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(
                query.GetType(), 
                typeof(TResponse));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = query;

            return handler.Handle(request);
        }
    }
}
