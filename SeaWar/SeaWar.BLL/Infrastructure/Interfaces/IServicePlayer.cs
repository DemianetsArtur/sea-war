using SeaWar.DAL.Infrastructure.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Interfaces
{
    public interface IServicePlayer
    {
        void OutputBoard(ICollection<Ship> ships);

        void PlaceShip(ICollection<Ship> ships);
    }
}
