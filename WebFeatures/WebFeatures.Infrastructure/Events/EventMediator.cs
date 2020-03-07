using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Infrastructure.Events
{
    public class EventMediator : IEventMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, Type> HandlersCache = new ConcurrentDictionary<Type, Type>();

        public EventMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TEvent>(TEvent eve) where TEvent : IEvent
        {
            Type handlerType = HandlersCache.GetOrAdd(
                typeof(TEvent),
                t => typeof(IEventHandler<>).MakeGenericType(t));

            IEnumerable<dynamic> handlers = _serviceProvider.GetServices(handlerType);

            // TODO: use hangfire ???
            foreach (var handler in handlers)
            {
                handler.Handle(eve);
            }
        }
    }
}
