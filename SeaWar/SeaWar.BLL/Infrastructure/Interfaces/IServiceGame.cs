using SeaWar.DAL.Infrastructure.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Interfaces
{
    public interface IServiceGame
    {
        void DisplayGame(ICollection<Ship> ships);
    }
}
