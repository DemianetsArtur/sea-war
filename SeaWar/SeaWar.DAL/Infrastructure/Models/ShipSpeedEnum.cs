using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public enum ShipSpeedEnum
    {
        [Description("1")]
        SubsidiarySpeed,

        [Description("2")]
        MixedSpeed,

        [Description("3")]
        MilitarySpeed
    }
}
