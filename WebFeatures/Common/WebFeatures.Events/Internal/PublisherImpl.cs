using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Events.Internal
{
    internal class PublisherImpl<TEvent> : Publisher
        where TEvent : IEvent
    {
        public override Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken)
        {
            var handlers = services.GetServices<IEventHandler<TEvent>>();

            return Task.WhenAll(handlers.Select(x => x.HandleAsync((TEvent)eve, cancellationToken)));
        }
    }
}