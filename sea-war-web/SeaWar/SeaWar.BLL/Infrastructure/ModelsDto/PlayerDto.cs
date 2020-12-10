using SeaWar.DAL.Entities;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.ModelsDto
{
    public class PlayerDto
    {
        public string Name { get; set; }

        public string ConnectionId { get; set; }

        public int HitPoints { get; set; }

        public int Count { get; set; }

        public IList<Coordinate> Coordinates { get; set; }

        public PlayerDto()
        {
            this.Coordinates = new List<Coordinate>();
        }
    }
}
