using SeaWar.DAL.Infrastructure.Models;
using System.Collections.Generic;

namespace SeaWar.DAL.Infrastructure.Entities
{
    public class Skill
    {
        public int Speed { get; set; }

        public int Range { get; set; }

        public ICollection<SkillTypeEnum> TypeSkill { get; set; }

        public Skill()
        {
            this.TypeSkill = new List<SkillTypeEnum>(); 
        }
    }
}
