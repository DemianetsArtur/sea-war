using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Extension;
using SeaWar.DAL.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class SubsidiaryBuilder : Ship
    {
        private ShipNameEnum shipNameEnum = ShipNameEnum.SubsidiaryName;

        private ShipWidthEnum shipWidthEnum = ShipWidthEnum.SubsidiaryWidth;

        private ShipSpeedEnum shipSpeedEnum = ShipSpeedEnum.SubsidiarySpeed;

        private ShipRangeEnum shipRangeEnum = ShipRangeEnum.SubsidiaryRange;
        public SubsidiaryBuilder()
        {
            string name = shipNameEnum.GetAttributOfType<DescriptionAttribute>().Description;
            int width = Int16.Parse(shipWidthEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int speed = Int16.Parse(shipSpeedEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int range = Int16.Parse(shipRangeEnum.GetAttributOfType<DescriptionAttribute>().Description);

            Name = name;
            Width = width;
            Type = PositionTypeEnum.Subsidiary;
            Skills = new List<Skill> 
            {
                new Skill { TypeSkill = new List<SkillTypeEnum>{ SkillTypeEnum.Fix }, 
                            Speed = speed, 
                            Range = range }
            };
        }
    }
}
