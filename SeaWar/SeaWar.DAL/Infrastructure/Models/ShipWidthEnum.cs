using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public enum ShipWidthEnum
    {
        [Description("2")]
        MilitaryWidth,

        [Description("1")]
        SubsidiaryWidth,

        [Description("1")]
        MixedWidth
    }
}
