using SeaWar.BLL.Infrastructure.Extensions;
using SeaWar.DAL.Infrastructure.Models;
using System.ComponentModel;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class Panel
    {
        public PositionTypeEnum positionTypeEnum { get; set; }

        public Cordinate Cordinate { get; set; }

        public Panel(int row, int column)
        {
            this.Cordinate = new Cordinate(row, column);
            this.positionTypeEnum = PositionTypeEnum.Empty;
        }

        public string Status 
        {
            get {
                return positionTypeEnum.GetAttributOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsStateShips 
        {
            get {
                return positionTypeEnum == PositionTypeEnum.Subsidiary 
                    || positionTypeEnum == PositionTypeEnum.Mixed 
                    || positionTypeEnum == PositionTypeEnum.Military;
            }
        }
    }
}