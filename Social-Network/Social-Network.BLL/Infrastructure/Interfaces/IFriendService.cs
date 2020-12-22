using System.Collections.Generic;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IFriendService
    {
        ICollection<UserAccountDto> FriendAll(string name);
    }
}