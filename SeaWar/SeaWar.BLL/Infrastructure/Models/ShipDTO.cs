using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class ShipDTO
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public PositionTypeEnum Type { get; set; }

        public bool IsSunk { get; set; }

        public IList<int> Coordinates { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public int StatusValue { get; set; }

        public ShipDTO()
        {
            this.Coordinates = new List<int>();
            this.Skills = new List<Skill>();
        }
    }
}
