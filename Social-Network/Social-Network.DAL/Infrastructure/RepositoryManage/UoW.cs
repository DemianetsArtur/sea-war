using Social_Network.DAL.Infrastructure.Interfaces;
using Social_Network.DAL.Infrastructure.Repositories;
using Social_Network.DAL.Manages.Tables;

namespace Social_Network.DAL.Infrastructure.RepositoryManage
{
    public class UoW : IUoW
    {
        private readonly TableManage _tableManage;

        public IUserAccountRepository UserAccount { get; }

        public UoW(TableManage tableManage)
        {
            this._tableManage = tableManage;
            this.UserAccount = new UserAccountRepository(this._tableManage);
        }
    }
}
