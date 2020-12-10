using SeaWar.DAL.DomainModel;
using SeaWar.DAL.Interfaces;

namespace SeaWar.DAL.Repository
{
    public class UoW : IUoW
    {
        private readonly IDataManage dataManage;
        
        public IPlayerRepository playerRepository { get; }

        public UoW(IDataManage dataManage)
        {
            this.dataManage = dataManage;
            this.playerRepository = new PlayerRepository(this.dataManage);
        }

    }
}
