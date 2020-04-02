using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Events
{
    public interface IEventMediator
    {
        Task PublishAsync(IEvent eve, CancellationToken cancellationToken = default);
    }
}
