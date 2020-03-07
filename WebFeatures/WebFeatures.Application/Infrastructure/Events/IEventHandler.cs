namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent eve);
    }
}
