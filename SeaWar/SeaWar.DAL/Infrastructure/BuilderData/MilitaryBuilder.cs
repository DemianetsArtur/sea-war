using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Extension;
using SeaWar.DAL.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class MilitaryBuilder : Ship
    {

        private ShipNameEnum shipNameEnum = ShipNameEnum.MilitaryName;

        private ShipWidthEnum shipWidthEnum = ShipWidthEnum.MilitaryWidth;

        private ShipSpeedEnum shipSpeedEnum = ShipSpeedEnum.MilitarySpeed;

        private ShipRangeEnum shipRangeEnum = ShipRangeEnum.MilitaryRange;

        public MilitaryBuilder()
        {
            string name = shipNameEnum.GetAttributOfType<DescriptionAttribute>().Description;
            int width = Int16.Parse(shipWidthEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int speed = Int16.Parse(shipSpeedEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int range = Int16.Parse(shipRangeEnum.GetAttributOfType<DescriptionAttribute>().Description);

            Name = name;
            Width = width;
            Type = PositionTypeEnum.Military;
            Skills = new List<Skill> 
            {
                new Skill{ TypeSkill = new List<SkillTypeEnum>{ SkillTypeEnum.Shoot }, 
                           Speed = speed , 
                           Range = range } 
            };
        }
    }
}
