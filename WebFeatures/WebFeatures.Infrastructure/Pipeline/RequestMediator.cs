using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Pipeline.Abstractions;

namespace WebFeatures.Infrastructure.Pipeline
{
    public class RequestMediator : IRequestMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, Type> HandlersCache = new ConcurrentDictionary<Type, Type>();

        public RequestMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command)
        {
            var handlerType = HandlersCache.GetOrAdd(
                command.GetType(), 
                x => typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse)));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = command;

            return handler.HandleAsync(request);
        }

        public Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query)
        {
            var handlerType = HandlersCache.GetOrAdd(
                query.GetType(),
                x => typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse)));

            dynamic handler = _serviceProvider.GetService(handlerType);
            dynamic request = query;

            return handler.HandleAsync(request);
        }
    }
}
