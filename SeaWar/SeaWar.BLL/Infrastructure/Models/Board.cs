using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Models
{
    public class Board
    {
        public ICollection<Panel> Panels { get; set; }

        public Board()
        {
            this.Panels = new List<Panel>();
            for (int i = 0; i <= ConfigBoard.sizeBoard; i++) 
            {
                for (int j = 0; j <= ConfigBoard.sizeBoard; j++) 
                {
                    this.Panels.Add(new Panel(i, j));
                }
            }
        }
    }
}
