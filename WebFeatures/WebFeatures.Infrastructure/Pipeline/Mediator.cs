using System;
using System.Collections.Concurrent;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Infrastructure.Pipeline
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, Type> HandlersCache = new ConcurrentDictionary<Type, Type>();

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResponse SendCommand<TResponse>(ICommand<TResponse> command)
        {
            var handlerType = HandlersCache.GetOrAdd(
                command.GetType(), 
                x => typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse)));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = command;

            return handler.Handle(request);
        }

        public TResponse SendQuery<TResponse>(IQuery<TResponse> query)
        {
            var handlerType = HandlersCache.GetOrAdd(
                query.GetType(),
                x => typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse)));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = query;

            return handler.Handle(request);
        }
    }
}
