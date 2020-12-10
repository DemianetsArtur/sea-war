using SeaWar.DAL.Entities;
using System.Collections.Generic;

namespace SeaWar.DAL.DomainModel
{
    public class DataManage : IDataManage
    {
        public ICollection<Player> Players { get; set; }

        public DataManage()
        {
            this.Players = new List<Player>();
        }
    }
}
