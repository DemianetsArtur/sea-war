using SeaWar.BLL.Infrastructure.Extensions;
using SeaWar.DAL.Infrastructure.Models;
using System.ComponentModel;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class Panel
    {
        public TypeShip TypeShip { get; set; }

        public Cordinate Cordinate { get; set; }

        public Panel(int row, int column)
        {
            this.Cordinate = new Cordinate(row, column);
            this.TypeShip = TypeShip.Empty;
        }

        public string Status 
        {
            get {
                return TypeShip.GetAttributOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsOccupid 
        {
            get {
                return TypeShip == TypeShip.Subsidiary 
                    || TypeShip == TypeShip.Mixed 
                    || TypeShip == TypeShip.Military;
            }
        }
    }
}
