namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventMediator
    {
        void Publish<TEvent>(TEvent eve) where TEvent : IEvent;
    }
}
