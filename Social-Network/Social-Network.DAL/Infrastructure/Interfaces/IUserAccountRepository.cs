using Social_Network.DAL.Entities;

namespace Social_Network.DAL.Infrastructure.Interfaces
{
    public interface IUserAccountRepository
    {
        void UserAccountCreate(UserAccount entity);

        bool UserAccountFind(UserAccount entity);

        UserAccount UserAccountLoginFind(UserAccount entity);

        void UserAccountReplace(string name, string imagePath);
    }
}
