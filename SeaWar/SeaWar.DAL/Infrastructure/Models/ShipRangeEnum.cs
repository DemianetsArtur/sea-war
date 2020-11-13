using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public enum ShipRangeEnum
    {
        [Description("3")]
        SubsidiaryRange,

        [Description("4")]
        MixedRange,

        [Description("5")]
        MilitaryRange
    }
}
