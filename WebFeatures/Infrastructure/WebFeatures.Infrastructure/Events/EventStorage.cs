using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Infrastructure.Events
{
    internal class EventStorage : IEventStorage
    {
        private static ConcurrentDictionary<Type, Publisher> PublishersCache
            = new ConcurrentDictionary<Type, Publisher>();

        private readonly IServiceProvider _services;
        private readonly List<IEvent> _events = new List<IEvent>();

        public EventStorage(IServiceProvider services)
        {
            _services = services;
        }

        public Task AddAsync(IEvent eve)
        {
            _events.Add(eve);

            return Task.CompletedTask;
        }

        public async Task PublishAllAsync(CancellationToken cancellationToken)
        {
            var publishingTasks = new List<Task>();

            foreach (IEvent eve in _events)
            {
                var publisher = PublishersCache.GetOrAdd(
                    eve.GetType(),
                    t =>
                    {
                        Type publisherType = typeof(Publisher<>).MakeGenericType(t);

                        Publisher publisher = (Publisher)Activator.CreateInstance(publisherType);

                        return publisher;
                    });

                Task task = publisher.PublishAsync(eve, _services, cancellationToken);

                publishingTasks.Add(task);
            }

            await Task.WhenAll(publishingTasks);

            _events.Clear();
        }
    }

    internal abstract class Publisher
    {
        public abstract Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken);
    }

    internal class Publisher<T> : Publisher
        where T : IEvent
    {
        public override Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken)
        {
            var tasks = services.GetServices<IEventHandler<T>>()
                .Select(x => x.HandleAsync((T)eve, cancellationToken));

            return Task.WhenAll(tasks);
        }
    }
}