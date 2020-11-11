namespace SeaWar.BLL.Infrastructure.Services
{
    public class ServiceGame
    {
        private ServicePlayer servicePlayer { get; set; }
        public ServiceGame()
        {
            this.servicePlayer = new ServicePlayer();
            this.servicePlayer.PlaceShip();
            this.servicePlayer.OutputBoard();
        }
    }
}
