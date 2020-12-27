using Social_Network.BLL.ModelsDto;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface INotificationService
    {
        void NotificationCreate(NotificationDto entity);
    }
}