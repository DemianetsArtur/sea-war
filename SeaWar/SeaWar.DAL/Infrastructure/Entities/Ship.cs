using SeaWar.DAL.Infrastructure.Models;
using System.Collections;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.Entities
{
    public class Ship
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public TypeShip Type { get; set; }

        public bool IsSunk { get; set; }

        public ICollection<Skill> Skills { get; set; }
        
        public Ship()
        {
            this.Skills = new List<Skill>();
        }
    }
}
