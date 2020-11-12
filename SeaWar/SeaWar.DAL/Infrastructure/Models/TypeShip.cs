using System.ComponentModel;

namespace SeaWar.DAL.Infrastructure.Models
{
    public static class ParameterShips
    {
        //Name Ships
        public static string NameMilitary = "Military";

        public static string NameSubsidiary = "Subsidiary";

        public static string NameMixed = "Mixed";

        //Speed Ships
        public static int SpeedMilitary = 3;

        public static int SpeedSubsidiary = 1;

        public static int SpeedMixed = 2;

        //Range Ships
        public static int RangeMilitary = 5;

        public static int RangeSubsidiary = 3;

        public static int RangeMixed = 4;

        //Width Ships
        public static int WidthMilitary = 2;

        public static int WidthSubsidiary = 1;

        public static int WidthMixed = 1;

    }

    public enum TypeShip
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
