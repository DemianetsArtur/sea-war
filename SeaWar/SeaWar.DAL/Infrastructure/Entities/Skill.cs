using SeaWar.DAL.Infrastructure.Models;

namespace SeaWar.DAL.Infrastructure.Entities
{
    public class Skill
    {
        public int Speed { get; set; }

        public int Range { get; set; }

        public TypeSkill TypeSkill { get; set; }
    }
}
