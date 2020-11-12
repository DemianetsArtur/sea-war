using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.DAL.Infrastructure.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Services
{
    public class ServiceGame : IServiceGame
    {
        private readonly IServicePlayer servicePlayer;
        public ServiceGame(IServicePlayer servicePlayer)
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
