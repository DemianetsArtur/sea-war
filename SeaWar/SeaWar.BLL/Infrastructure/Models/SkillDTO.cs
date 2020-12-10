using SeaWar.DAL.Infrastructure.Models;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class SkillDTO
    {
        public int Speed { get; set; }

        public int Range { get; set; }

        public SkillTypeEnum TypeSkill { get; set; }
    }
}
