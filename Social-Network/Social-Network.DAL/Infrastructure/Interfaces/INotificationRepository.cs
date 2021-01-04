using System.Collections.Generic;
using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface INotificationRepository
    {
        void EventAddToFriend(Notification entity);
        ICollection<Notification> GetEventAddToFriend();
        void EventAddToFriendRemove(Notification entity);
        void EventMessageCreate(Notification entity);
        void EventMessagesRemove(Notification entity);
        Notification EventMessagesGet(Notification entity);
    }
}