using System.Collections.Generic;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.BLL.Infrastructure.Interfaces
{
    public interface IUserAccountService
    {
        void UserAccountCreate(UserAccountDto entity);

        bool UserAccountFind(UserAccountDto entity);

        UserAccountDto UserAccountLoginFind(UserAccountDto entity);

        void UserAccountReplace(string name, string imagePath);

        UserAccountDto GetUser(string name);

        void UserChangedCreate(UserAccountDto entity);

        ICollection<UserAccountDto> UserAll();
    }
}
