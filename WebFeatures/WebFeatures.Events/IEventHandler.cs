using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent eve, CancellationToken cancellationToken);
    }
}
