using SeaWar.DAL.Entities;
using System.Collections.Generic;

namespace SeaWar.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        void Add(Player entity);

        ICollection<Player> GetAll();

        void HitPointUpdate(Player entity);

        void CountUpdate(Player entity);

        void CoordinateCreate(Player entity);

        void PlayerRemove(Player entity);
    }
}
