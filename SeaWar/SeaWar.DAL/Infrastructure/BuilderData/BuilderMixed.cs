using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class BuilderMixed : Ship
    {
        public BuilderMixed()
        {
            Name = ParameterShips.NameMixed;
            Width = ParameterShips.WidthMixed;
            Type = TypeShip.Mixed;
            Skills = new List<Skill> 
            { 
               new Skill { TypeSkill = TypeSkill.Repairs, Range = ParameterShips.RangeMixed, Speed = ParameterShips.SpeedMixed },
               new Skill { TypeSkill = TypeSkill.Shot  }
            };
        }
    }
}
