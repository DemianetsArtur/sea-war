using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public enum ShipNameEnum
    {
        [Description("Military")]
        MilitaryName,

        [Description("Subsidiary")]
        SubsidiaryName,

        [Description("Mixed")]
        MixedName
    }
}
