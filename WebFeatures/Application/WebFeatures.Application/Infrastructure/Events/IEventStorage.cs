using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventStorage
    {
        void Add(IEvent eve);
        Task PublishAllAsync(CancellationToken cancellationToken);
    }
}
