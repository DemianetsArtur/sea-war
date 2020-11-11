using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class ShipDTO
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public TypeShip Type { get; set; }

        public bool IsSunk { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ShipDTO()
        {
            this.Skills = new List<Skill>();
        }
    }
}
