using SeaWar.DAL.Infrastructure.Entities;
using System.Collections.Generic;

namespace SeaWar.UI.Infrastructure.Interfaces
{
    public interface IShipService
    {
        ICollection<Ship> MessageAddShips();
    }
}
