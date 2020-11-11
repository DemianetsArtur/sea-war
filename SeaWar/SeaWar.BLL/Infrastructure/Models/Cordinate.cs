namespace SeaWar.BLL.Infrastructure.Models
{
    public class Cordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Cordinate(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
