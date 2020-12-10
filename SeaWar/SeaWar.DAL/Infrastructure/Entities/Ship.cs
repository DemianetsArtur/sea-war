using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.Entities
{
    public class Ship
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public PositionTypeEnum Type { get; set; }

        public bool IsSunk { get; set; }

        public IList<int> Coordinates { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public string Location { get; set; }
        
        public Ship()
        {
            this.Coordinates = new List<int>();
            this.Skills = new List<Skill>();
        }
    }
}
