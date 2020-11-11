using SeaWar.BLL.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace SeaWar.BLL.Infrastructure.Extensions
{
    public static class ExtensionPanel
    {
        public static Panel At(this ICollection<Panel> panels, int row, int column) 
        {
            return panels
                   .Where(opt => opt.Cordinate.Row == row && opt.Cordinate.Column == column)
                   .First();
        }

        public static ICollection<Panel> Range(this ICollection<Panel> panel, 
                                                    int startRow, 
                                                    int startColumn, 
                                                    int endRow, 
                                                    int endColumn)
        {
            var res =  panel
                   .Where(opt => opt.Cordinate.Row >= startRow 
                          && opt.Cordinate.Column >= startColumn 
                          && opt.Cordinate.Row <= endRow 
                          && opt.Cordinate.Column <= endColumn)
                   .ToList();
            return res;
        }
    }
}
