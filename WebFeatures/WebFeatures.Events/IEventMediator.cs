using System.Threading.Tasks;

namespace WebFeatures.Events
{
    public interface IEventMediator
    {
        Task PublishAsync<TEvent>(TEvent eve) where TEvent : IEvent;
    }
}
