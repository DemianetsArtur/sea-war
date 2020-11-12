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
               new Skill { TypeSkill = new List<TypeSkill>{ TypeSkill.Repairs, TypeSkill.Shot }, Range = ParameterShips.RangeMixed, Speed = ParameterShips.SpeedMixed }
            };
        }
    }
}
