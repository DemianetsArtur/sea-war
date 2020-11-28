using SeaWar.DAL.Entities;
using System.Collections.Generic;

namespace SeaWar.DAL.DomainModel
{
    public interface IDataManage
    {
        ICollection<Player> Players { get; set; }
    }
}
