using AutoMapper;
using SeaWar.BLL.Infrastructure.Extensions;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.DAL.Infrastructure.BuilderData;
using SeaWar.DAL.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeaWar.BLL.Infrastructure.Services
{
    public class ServicePlayer : IServicePlayer
    {
        private ICollection<Ship> Ships { get; set; }
        private Board Board { get; set; }
        private readonly IMapper mapper;
        public ServicePlayer(IMapper mapper)
        {
            this.Ships = new List<Ship>() 
            {
                new BuilderMilitary(),
                new BuilderSubsidiary(),
                new BuilderMixed()
            };
            this.mapper = mapper;
            this.Board = new Board();
        }

        public void OutputBoard(ICollection<Ship> ships) 
        {
            this.ShowSortedData(ships);

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

            this.GetShipByCoordinate(ships);
        }

        public void PlaceShip(ICollection<Ship> ships) 
        {
            int data = Guid.NewGuid().GetHashCode();
            Random rnd = new Random(data);
            int startColumn = 0;
            int startRow = 0;
            foreach (Ship entity in ships) 
            {

                bool isOpen = true;
                while (isOpen) 
                {
                    for (int i = 0; i < entity.Coordinates.Count(); i++) 
                    {
                        startColumn = entity.Coordinates[i];
                        i++;
                        startRow = entity.Coordinates[i];
                    }

                    
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

        private void  GetShipByCoordinate(ICollection<Ship> ships) 
        {
            string msg = null;
            bool isOpen = true;
            IList<int> listCoordinates = new List<int>();
            Ship ship = new Ship();
            string isSunk = (ship.IsSunk) ? InfoState.IsSunk : InfoState.Alive;

            while (isOpen)
            {
                Console.WriteLine();
                Console.Write("View ship states, enter first coordinate: ");
                msg = Console.ReadLine();

                if (!this.IsValidInput(msg))
                {
                    listCoordinates.Clear();
                    Console.WriteLine("You did not enter the coordinates correctly, enter again");
                    isOpen = true;
                    continue;
                }
                else 
                {
                    listCoordinates.Add(Int32.Parse(msg));
                }

                Console.Write("Enter second coordinate: ");
                msg = Console.ReadLine();

                if (!this.IsValidInput(msg))
                {
                    listCoordinates.Clear();
                    Console.WriteLine("You did not enter the coordinates correctly, enter again");
                    isOpen = true;
                    continue;
                }
                else
                {
                    listCoordinates.Add(Int32.Parse(msg));
                }

                foreach (var entity in ships) 
                {
                    var isEqualCoordinate = listCoordinates.All(opt => entity.Coordinates.Contains(opt));
                    if (isEqualCoordinate && listCoordinates.Count() != 0)
                    {
                        ship = entity;
                        isEqualCoordinate = false;
                        listCoordinates.Clear();
                    }

                    
                }

                if (ship == null)
                {
                    Console.WriteLine("You entered the wrong coordinates. try again");
                    isOpen = true;
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine($"Name: {ship.Name}");
                Console.WriteLine($"Type: {ship.Type}");
                foreach (var skill in ship.Skills)
                {
                    Console.WriteLine($" Speed: {skill.Speed}\n Range: {skill.Range}");
                    Console.Write($" Type Skill:");
                    foreach (var typeSkill in skill.TypeSkill)
                    {
                        Console.Write($" {typeSkill}, ");

                    }
                }

                Console.WriteLine();
                Console.Write(" Coordenate: ");
                foreach (var coord in ship.Coordinates)
                {
                    Console.Write($" {coord}, ");
                }
                Console.WriteLine();
                Console.WriteLine($" {isSunk}");

                if (this.IsInputCorrect()) 
                {
                    isOpen = true;
                    continue;
                }

                isOpen = false;
            }  
        }

        private bool IsInputCorrect() 
        {
            Console.WriteLine();

            string msg = null;
            string correctAnswer = "Yes";
            string shortAnswer = "Y";
            Console.WriteLine();
            Console.Write("View the status of another ship Enter: Yes/If not any other key: ");
            msg = Console.ReadLine();
            if (msg == correctAnswer.ToLower()
             || msg == correctAnswer.ToUpper()
             || msg == correctAnswer
             || msg == shortAnswer.ToLower()
             || msg == shortAnswer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsValidInput(string msg)
        {
            Regex pattern = new Regex(InfoParameter.regExPattern);
            bool isValid = pattern.IsMatch(msg);

            if (!isValid) 
            {
                return false;
            }

            int input = Int16.Parse(msg);
            return (input >= ConfigBoard.sizeBoard) ? false : true;
        }

        private void ShowSortedData(ICollection<Ship> ships) 
        {
            int averageValue = 0;
            int coutShip = 0;
            IList<ShipDTO> listShip = this.mapper.Map<IList<ShipDTO>>(ships);

            foreach (var res in ships)
            {
                bool isOpen = true;

                while (isOpen)
                {
                    for (int i = 1; i <= res.Coordinates.Count(); i++)
                    {
                        averageValue = res.Coordinates[i];
                        i++;

                    }

                    listShip[coutShip].StatusValue = averageValue;
                    coutShip++;
                    isOpen = false;
                }
            }

            ICollection<ShipDTO> sortedData = listShip.OrderByDescending(opt => 
                                                                           opt.StatusValue >= ConfigBoard.minSize 
                                                                           && opt.StatusValue <= ConfigBoard.maxSize 
                                                                           || opt.StatusValue == ConfigBoard.averageSize)
                                                      .ToList();

            Console.WriteLine("Ships: ");
            foreach (var sortedShips in sortedData)
            {
                Console.Write($"Name: {sortedShips.Name }, ");
                foreach (var coordinate in sortedShips.Coordinates)
                {
                    Console.Write("[");
                    Console.Write($"{coordinate}");
                    Console.Write("]");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
