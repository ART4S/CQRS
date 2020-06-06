using System.Threading;
using System.Threading.Tasks;

namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventStorage
    {
        Task AddAsync(IEvent eve);
        Task PublishAllAsync(CancellationToken cancellationToken);
    }
}
