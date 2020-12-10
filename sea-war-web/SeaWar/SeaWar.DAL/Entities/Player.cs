using System.Collections.Generic;

namespace SeaWar.DAL.Entities
{
    public class Player
    {
        public string Name { get; set; }

        public string ConnectionId { get; set; }

        public int HitPoints { get; set; }

        public int Count { get; set; }

        public IList<Coordinate> Coordinates { get; set; }

        public Player()
        {
            this.Coordinates = new List<Coordinate>();
        }
    }
}
