using SeaWar.DAL.DomainModel;
using SeaWar.DAL.Entities;
using SeaWar.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SeaWar.DAL.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDataManage dataManage;

        public PlayerRepository(IDataManage dataManage)
        {
            this.dataManage = dataManage; 
        }

        public ICollection<Player> GetAll() 
        {
            return this.dataManage.Players;
        }

        public void Add(Player entity) 
        {
            var playerValid = this.dataManage.Players.FirstOrDefault(opt => opt.Name == entity.Name);
            if (playerValid == null) {
                this.dataManage.Players.Add(entity);
            }
        }

        public void HitPointUpdate(Player entity) 
        {
            this.dataManage.Players.FirstOrDefault(opt => opt.Name == entity.Name).HitPoints = entity.HitPoints;
        }

        public void CoordinateCreate(Player entity) 
        {
            this.dataManage.Players.FirstOrDefault(opt => opt.Name == entity.Name).Coordinates = entity.Coordinates;
        }

        public void CountUpdate(Player entity) 
        {
            this.dataManage.Players.FirstOrDefault(opt => opt.Name == entity.Name).Count = entity.Count;
        }

        public void PlayerRemove(Player entity) 
        {
            this.dataManage.Players.Clear();
        }
    }
}
