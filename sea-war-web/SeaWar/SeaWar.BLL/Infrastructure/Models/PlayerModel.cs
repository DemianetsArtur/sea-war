using SeaWar.DAL.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }

        public IList<Coordinate> Coordinates { get; set; }

        public int HitPoints { get; set; }

        public int Count { get; set; }

        public PlayerModel()
        {
            this.Coordinates = new List<Coordinate>();
        }
    }
}
