namespace WebFeatures.Application.Infrastructure.Events
{
    public interface INotificationHandler<TNotification> 
        where TNotification : INotification
    {
        void Handle(TNotification notification);
    }
}
