using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface INotificationRepository
    {
        void NotificationCreate(Notification entity);
    }
}