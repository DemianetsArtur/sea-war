using System.Collections.Generic;
using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IMessageRepository
    {
        void MessageCreate(Message entity);

        ICollection<Message> MessageAll(Message entity);
    }
}