using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent eve, CancellationToken cancellationToken);
    }
}
