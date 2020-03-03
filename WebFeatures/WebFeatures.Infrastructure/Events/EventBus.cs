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
        private readonly ConcurrentDictionary<Type, Type> _handlersCache = new ConcurrentDictionary<Type, Type>();

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            Type handlerType = _handlersCache.GetOrAdd(
                typeof(TNotification),
                t => typeof(INotificationHandler<>).MakeGenericType(t));

            IEnumerable<dynamic> handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                handler.Handle(notification);
            }
        }
    }
}
