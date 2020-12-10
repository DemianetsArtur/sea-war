using System.Collections.Generic;

namespace SeaWar.API.Infrastructure.Models
{
    public class Player
    {
        public string Name { get; set; }

        public int Status { get; set; }

        public string OpponentName { get; set; }

        public IList<Coordinate> Coordinates { get; set; }

        public Player()
        {
            this.Coordinates = new List<Coordinate>();
        }
    }
}
