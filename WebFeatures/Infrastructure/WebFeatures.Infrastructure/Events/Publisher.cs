using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Infrastructure.Events;

namespace WebFeatures.Infrastructure.Events
{
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