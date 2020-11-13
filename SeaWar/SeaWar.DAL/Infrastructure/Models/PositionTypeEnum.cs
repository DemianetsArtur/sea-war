using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public enum PositionTypeEnum
    {
        [Description("o")]
        Empty,

        [Description(".")]
        Military,

        [Description(".")]
        Subsidiary,

        [Description(".")]
        Mixed
    }
}
