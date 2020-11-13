using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.DAL.Infrastructure.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IPlayerService servicePlayer;
        public GameService(IPlayerService servicePlayer)
        {
            this.servicePlayer = servicePlayer;
        }

        public void DisplayGame(ICollection<Ship> ships) 
        {
            this.servicePlayer.PlaceShip(ships);
            this.servicePlayer.OutputBoard(ships);
        }
    }
}
