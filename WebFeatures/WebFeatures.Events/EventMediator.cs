using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace WebFeatures.Events
{
    internal class EventMediator : IEventMediator
    {
        private readonly IServiceProvider _services;
        private static readonly ConcurrentDictionary<Type, Publisher> Publishers = new ConcurrentDictionary<Type, Publisher>();

        public EventMediator(IServiceProvider services)
        {
            _services = services;
        }

        public Task PublishAsync(IEvent eve)
        {
            Publisher publisher = Publishers.GetOrAdd(
                eve.GetType(),
                _ =>
                {
                    Type publisherType = typeof(PublisherImpl<>).MakeGenericType(eve.GetType());
                    return (Publisher)Activator.CreateInstance(publisherType);
                });

            return publisher.PublishAsync(eve, _services);
        }
    }

    internal abstract class Publisher
    {
        public abstract Task PublishAsync(IEvent eve, IServiceProvider services);
    }

    internal class PublisherImpl<TEvent> : Publisher
        where TEvent : IEvent
    {
        public override Task PublishAsync(IEvent eve, IServiceProvider services)
        {
            var handlers = services.GetServices<IEventHandler<TEvent>>();

            return Task.WhenAll(handlers.Select(x => x.HandleAsync((TEvent)eve)));
        }
    }
}