using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class BuilderMilitary : Ship
    {
        public BuilderMilitary()
        {
            Name = ParameterShips.NameMilitary;
            Width = ParameterShips.WidthMilitary;
            Type = TypeShip.Military;
            Skills = new List<Skill> 
            {
                new Skill{ TypeSkill = TypeSkill.Shot, Speed  = ParameterShips.SpeedMilitary, Range = ParameterShips.RangeMilitary } 
            };
        }
    }
}
