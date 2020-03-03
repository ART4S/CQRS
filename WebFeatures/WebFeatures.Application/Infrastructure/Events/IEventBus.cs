namespace WebFeatures.Application.Infrastructure.Events
{
    public interface IEventBus
    {
        void Publish<TNotification>(TNotification notification) where TNotification : INotification;
    }
}
