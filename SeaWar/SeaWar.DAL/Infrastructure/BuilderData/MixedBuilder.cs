using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.DAL.Infrastructure.Extension;
using SeaWar.DAL.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.BuilderData
{
    public class MixedBuilder : Ship
    {
        private ShipNameEnum shipNameEnum = ShipNameEnum.MixedName;

        private ShipWidthEnum shipWidthEnum = ShipWidthEnum.MixedWidth;

        private ShipSpeedEnum shipSpeedEnum = ShipSpeedEnum.MixedSpeed;

        private ShipRangeEnum shipRangeEnum = ShipRangeEnum.MixedRange;

        public MixedBuilder()
        {
            string name = shipNameEnum.GetAttributOfType<DescriptionAttribute>().Description;
            int width = Int16.Parse(shipWidthEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int speed = Int16.Parse(shipSpeedEnum.GetAttributOfType<DescriptionAttribute>().Description);
            int range = Int16.Parse(shipRangeEnum.GetAttributOfType<DescriptionAttribute>().Description);

            Name = name;
            Width = width;
            Type = PositionTypeEnum.Mixed;
            Skills = new List<Skill> 
            { 
               new Skill { TypeSkill = new List<SkillTypeEnum>{ SkillTypeEnum.Fix, SkillTypeEnum.Shoot }, 
                           Range = speed, 
                           Speed = range }
            };
        }
    }
}
