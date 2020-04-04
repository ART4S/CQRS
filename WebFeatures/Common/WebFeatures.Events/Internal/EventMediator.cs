using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Events.Internal;

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

        public Task PublishAsync(IEvent eve, CancellationToken cancellationToken = default)
        {
            Publisher publisher = Publishers.GetOrAdd(
                eve.GetType(),
                _ =>
                {
                    Type publisherType = typeof(PublisherImpl<>).MakeGenericType(eve.GetType());
                    return (Publisher)Activator.CreateInstance(publisherType);
                });

            return publisher.PublishAsync(eve, _services, cancellationToken);
        }
    }
}