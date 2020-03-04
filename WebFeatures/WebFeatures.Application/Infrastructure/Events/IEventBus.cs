namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventBus
    {
        void Publish<TEventMessage>(TEventMessage message) where TEventMessage : IEventMessage;
    }
}
