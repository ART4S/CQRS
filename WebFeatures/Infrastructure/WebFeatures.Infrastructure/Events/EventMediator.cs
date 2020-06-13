using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Infrastructure.Events
{
    internal class EventMediator : IEventMediator
    {
        private static ConcurrentDictionary<Type, Publisher> PublishersCache
            = new ConcurrentDictionary<Type, Publisher>();

        private readonly IServiceProvider _services;

        public EventMediator(IServiceProvider services)
        {
            _services = services;
        }

        public Task PublishAsync(IEvent eve, CancellationToken cancellationToken)
        {
            Publisher publisher = PublishersCache.GetOrAdd(
                eve.GetType(),
                t =>
                {
                    Type publisherType = typeof(Publisher<>).MakeGenericType(t);

                    Publisher publisher = (Publisher)Activator.CreateInstance(publisherType);

                    return publisher;
                });

            return publisher.PublishAsync(eve, _services, cancellationToken);
        }
    }

    internal abstract class Publisher
    {
        public abstract Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken);
    }

    internal class Publisher<T> : Publisher where T : IEvent
    {
        public override Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken)
        {
            var tasks = services.GetServices<IEventHandler<T>>()
                .Select(x => x.HandleAsync((T)eve, cancellationToken));

            return Task.WhenAll(tasks);
        }
    }
}