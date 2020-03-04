using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, Type> HandlersCache = new ConcurrentDictionary<Type, Type>();

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : IEventMessage
        {
            Type handlerType = HandlersCache.GetOrAdd(
                typeof(TNotification),
                t => typeof(IEventMessageHandler<>).MakeGenericType(t));

            IEnumerable<dynamic> handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                handler.Handle(notification);
            }
        }
    }
}
