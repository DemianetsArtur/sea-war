using AutoMapper;
using SeaWar.BLL.Infrastructure.Models;
using SeaWar.DAL.Infrastructure.BuilderData;
using SeaWar.DAL.Infrastructure.Entities;
using SeaWar.UI.Infrastructure.Interfaces;
using SeaWar.UI.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeaWar.UI.Infrastructure.Services
{
    public class ServiceMessageAddShips : IServiceMessageAddShip
    {
        private ICollection<Ship> Ships { get; set; }
        
        private IList<int> ListCoordinates { get; set; }

        private string NameShip { get; set; }

        private IList<Ship> ListShips { get; set; }

        private readonly IMapper mapper;

        public ServiceMessageAddShips(IMapper mapper)
        {
            this.Ships = new List<Ship>()
            {
                new BuilderMilitary(),
                new BuilderSubsidiary(),
                new BuilderMixed()
            };
            this.ListCoordinates = new List<int>();
            this.ListShips = new List<Ship>();
            this.mapper = mapper; 
        }

        public ICollection<Ship> MessageAddShips() 
        {
            string msg = null;
            bool isOpen = true;
            bool isCorrectCoordinate = true;
            bool isOpenCoordinate = true;
            int countShip = 0;
            bool isCorrectCount = true;
            bool isCorrectLocationShip = true;

            while (isOpen) 
            {
                Console.WriteLine();
                Console.Write("Write the ship you want to add: ");
                msg = Console.ReadLine();

                bool isCorrectShipType = this.IsCorrectTypeShip(this.Ships, msg);
                if (!isCorrectShipType) 
                {
                    Console.WriteLine("You entered the name incorrectly, please enter it again");
                    isOpen = true;
                    continue;
                }

                this.NameShip = msg;

                

                while (isOpenCoordinate) 
                {
                    Console.WriteLine();
                    Console.WriteLine("Set coordinates for the ship: ");
                    Console.Write("Set first coordinate: ");
                    msg = Console.ReadLine();

                    isCorrectCoordinate = this.IsCorrectCoordinates(msg);
                    if (!isCorrectCoordinate)
                    {
                        Console.WriteLine("You did not enter the coordinates correctly, enter again");
                        isOpenCoordinate = true;
                        continue;
                    }
                    else
                    {
                        ListCoordinates.Add(Int16.Parse(msg));
                    }

                    Console.Write("Set second coordinate: ");
                    msg = Console.ReadLine();
                    isCorrectCoordinate = this.IsCorrectCoordinates(msg);
                    if (!isCorrectCoordinate)
                    {
                        Console.WriteLine("You did not enter the coordinates correctly, enter again");
                        isOpenCoordinate = true;
                        continue;
                    }
                    else
                    {
                        ListCoordinates.Add(Int16.Parse(msg));
                    }
                    
                    
                    isOpenCoordinate = false;
                }

                countShip = this.ListShips.Count();
                isCorrectCount = this.SetShipParameters(this.Ships, this.NameShip, this.ListCoordinates.ToList(), countShip); 
                if (!isCorrectCount) 
                {
                    Console.WriteLine("You can add a ship of the same type more than three times");
                    isOpen = true;
                    isOpenCoordinate = true;
                    continue;
                }

                isCorrectLocationShip = this.LocationShip(this.NameShip, countShip);
                if (!isCorrectLocationShip)
                {
                    Console.WriteLine("Choose other coordinates there is a ship nearby");
                    isOpen = true;
                    isOpenCoordinate = true;
                    continue;
                }

                this.ListCoordinates.Clear();

                if (this.IsCorrectKey()) 
                {
                    isOpen = true;
                    isOpenCoordinate = true;
                    continue;
                }

                isOpen = false;
            }

            Console.Clear();
            return this.ListShips
                       .ToList();
        }


        private bool IsCorrectKey() 
        {
            this.GetInfoStateShip();

            string msg = null;
            string correctAnswer = "Yes";
            string shortAnswer = "Y";
            Console.WriteLine();
            Console.WriteLine("Add more ships: ");
            Console.Write("Enter: Yes/If not any other key: ");
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

        private void GetInfoStateShip() 
        {
            Ship ship = this.GetParameterShip(this.NameShip);
            string isSunk = (ship.IsSunk) ? InfoStateShip.IsSunk : InfoStateShip.Alive;
            Console.Clear();
            Console.WriteLine("Ship parameters: ");

            Console.WriteLine($" Name: {ship.Name} \n");
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
        }

        private Ship GetParameterShip(string name) 
        {
            var sortedData = this.ListShips.Where(opt => opt.Name.ToUpper() == name
                                               || opt.Name.ToLower() == name
                                               || opt.Name == name)
                                           .LastOrDefault();
            return sortedData;
        }

        private bool IsCorrectCoordinates(string coordinate) 
        {
            Regex pattern = new Regex(InfoStateShip.regExPattern);
            bool isValid =  pattern.IsMatch(coordinate);

            if (!isValid) 
            {
                this.ListCoordinates.Clear();
                return false;
            }

            int input = Int16.Parse(coordinate);
            if (input >= ConfigBoard.sizeBoard)
            {
                this.ListCoordinates.Clear();
                return false;
            }

            else
            {
                return true;
            }
        }

        private bool IsCorrectTypeShip(ICollection<Ship> ships, string msg) 
        {
            var sortedData = ships.Where(opt => opt.Name.ToUpper() == msg 
                              || opt.Name.ToLower() == msg 
                              || opt.Name == msg)
                          .FirstOrDefault();
            return (sortedData == null) ? false : true; 
        }

        private bool SetShipParameters(ICollection<Ship> ships, string name, IList<int> coordinate, int countShip) 
        {
            var shipCount = this.ListShips.Where(opt => opt.Name.ToUpper() == name
                              || opt.Name.ToLower() == name
                              || opt.Name == name).ToList();

            if (shipCount.Count() == InfoStateShip.LimitCreateShip) 
            {
                this.ListCoordinates.Clear();
                return false;
            }

            if (this.ListShips.Count() != 0) 
            {
                foreach (var entity in this.ListShips)
                {
                        var isEqualCoordinate = coordinate.All(opt => entity.Coordinates.Contains(opt));
                        if (isEqualCoordinate)
                        {
                            this.ListCoordinates.Clear();
                            return false;
                        }
                }
            }

            var sortedData = ships.Where(opt => opt.Name.ToUpper() == name
                              || opt.Name.ToLower() == name
                              || opt.Name == name)
                          .FirstOrDefault();
            this.ListShips.Insert(countShip, new Ship { Coordinates = coordinate, 
                                                        Name = sortedData.Name, 
                                                        Skills = sortedData.Skills, 
                                                        Type = sortedData.Type, 
                                                        IsSunk = sortedData.IsSunk, 
                                                        Width = sortedData.Width});
            return true;
        }

        private bool LocationShip(string name, int index) 
        {
            IList<int> coordinate = new List<int>();
            int coord = 0;

            foreach (var entity in this.ListShips) 
            {
                for (int i = 0; i < entity.Coordinates.Count(); i++)
                {
                    coord = entity.Coordinates[i] - 1;
                    coordinate.Add(coord);
                }

                var isEqualCoordinate = entity.Coordinates.All(opt => coordinate.Contains(opt)); 
                if (isEqualCoordinate)
                {
                    this.ListCoordinates.Clear();
                    this.ListShips.RemoveAt(index);
                    return false;
                }

            }

            return true;
        }
    }
}
