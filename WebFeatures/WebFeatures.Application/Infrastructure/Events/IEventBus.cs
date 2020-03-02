namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventBus
    {
        void Publish<TNotification>() where TNotification : INotification;
    }
}
