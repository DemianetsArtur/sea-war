using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class BuilderSubsidiary : Ship
    {
        public BuilderSubsidiary()
        {
            Name = ParameterShips.NameSubsidiary;
            Width = ParameterShips.WidthSubsidiary;
            Type = TypeShip.Subsidiary;
            Skills = new List<Skill> 
            {
                new Skill { TypeSkill = TypeSkill.Repairs, Speed = ParameterShips.SpeedSubsidiary, Range = ParameterShips.RangeSubsidiary }
            };
        }
    }
}
