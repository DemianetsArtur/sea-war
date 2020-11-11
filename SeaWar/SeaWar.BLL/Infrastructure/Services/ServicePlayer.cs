using SeaWar.BLL.Infrastructure.Extensions;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.DAL.Infrastructure.BuilderData;
using SeaWar.DAL.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaWar.BLL.Infrastructure.Services
{
    public class ServicePlayer
    {
        private ICollection<Ship> Ships { get; set; }
        private Board Board { get; set; }
        public ServicePlayer()
        {
            this.Ships = new List<Ship>() 
            {
                new BuilderMilitary(),
                new BuilderSubsidiary(),
                new BuilderMixed()
            };

            this.Board = new Board();
        }

        public void OutputBoard() 
        {
            Console.WriteLine("Sea War");
            Console.WriteLine();
            for (int row = 1; row <= ConfigBoard.sizeBoard; row++) 
            {
                for (int column = 1; column <= ConfigBoard.sizeBoard; column++) 
                {
                    Console.Write(this.Board.Panels.At(row, column).Status + " ");
                }
                
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        ///Now in development
        public void PlaceShip() 
        {
            int data = Guid.NewGuid().GetHashCode();
            Random rnd = new Random(data);
            foreach (Ship entity in this.Ships) 
            {
                bool isOpen = true;
                while (isOpen) 
                {
                    int startColumn = rnd.Next(1, 11);
                    int startRow = rnd.Next(1, 11);
                    int endRow = startRow;
                    int endColumn = startColumn;
                    int orientation = rnd.Next(1, 101) % 2;

                    if (orientation == 0)
                    {
                        for (int i = 1; i < entity.Width; i++)
                        {
                            endRow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < entity.Width; i++)
                        {
                            endColumn++;
                        }
                    }

                    if (endRow > 10 || endColumn > 10) 
                    {
                        isOpen = true;
                        continue;
                    }

                    ICollection<Panel> affectedPanels = this.Board.Panels.Range(startRow, 
                                                                                startColumn, 
                                                                                endRow, 
                                                                                endColumn);
                    if (affectedPanels.Any(opt => opt.IsOccupid)) 
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (Panel panel in affectedPanels) 
                    {
                        panel.TypeShip = entity.Type;
                    }

                    isOpen = false;
                }
            }

        }
    }
}
