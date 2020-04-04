using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Events.Internal
{
    internal abstract class Publisher
    {
        public abstract Task PublishAsync(IEvent eve, IServiceProvider services, CancellationToken cancellationToken);
    }
}