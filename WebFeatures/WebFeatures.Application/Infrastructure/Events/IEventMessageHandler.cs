namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventMessageHandler<TEventMessage> 
        where TEventMessage : IEventMessage
    {
        void Handle(TEventMessage message);
    }
}
