using System.Collections.Generic;
using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IFriendRepository
    {
        ICollection<UserAccount> FriendAll(string name);
    }
}