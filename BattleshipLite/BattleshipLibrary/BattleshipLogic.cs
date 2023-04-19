using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public class BattleshipLogic
    {
        public static void InitializeShotGrid(PlayerModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }

            }
        }

        public static bool PlaceShip(PlayerModel model, string location)
        {
           
            (string row, int column) = SplitShotByRowAndColumn(location);
            //if (!int.TryParse(location.Substring(1, 1), out number) || string.IsNullOrEmpty(letter))
            //{
            //    return false;
            //}
            bool output = false;
            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipSpot(model, row, column);
            
            

            if (isValidLocation && isSpotOpen)
            {
                GridSpotModel gridSpot = new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                };
                model.ShipLocation.Add(gridSpot);
                output = true;
            }
           
            return output;
        }

        // Check if there is a ship already
        private static bool ValidateShipSpot(PlayerModel model, string row, int column)
        {
            bool isValidLocation = true;

            foreach (var ship in model.ShipLocation)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false; break;
                }
            }
            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerModel model, string row, int column)
        {
            bool isValidGrid = false;

            foreach (var ship in model.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidGrid = true; break;
                }
            }
            return isValidGrid;
        }

        private static void AddGridSpot(PlayerModel model, string letter, int number)
        {
            GridSpotModel gridSpot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(gridSpot);
        }
        public static void AddShipPlcae(PlayerModel model, string location, GridSpotStatus status)
        {
            (string letter, int number) = SplitShotByRowAndColumn(location);


            GridSpotModel grid = new GridSpotModel();
            grid.SpotLetter = letter;
            grid.SpotNumber = number;
            grid.Status = GridSpotStatus.Ship;
            model.ShipLocation.Add(grid);
        }

        public static bool IsGameOver(PlayerModel opponent)
        {
            // Player Still Active
            bool isGameOver = true;

            foreach (var ship in opponent.ShipLocation)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    // At least one has not sunk.
                    isGameOver = false;
                }
            }
            return isGameOver;
        }

        public static int GetShotCount(PlayerModel player)
        {
            int shotCount = 0;
            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    // Either miss or hit
                    shotCount++;
                }
            }
            return shotCount;
        }

        public static (string row, int column) SplitShotByRowAndColumn(string shot)
        {
            string row = string.Empty;
            int column = 0;
            
            if (shot.Length != 2)
            {
                throw new ArgumentException("This shot was invalid type", "shot");
            }

            char[] shotArray = shot.ToArray();
            row = shotArray[0].ToString();
            column =   int.Parse(shotArray[1].ToString()); ;

            return (row, column);
        }

        public static bool ValidateShot(PlayerModel player, string row, int column)
        {
            bool isValidShot = false;

            foreach (var ship in player.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column && ship.Status == GridSpotStatus.Empty)
                {
                    isValidShot = true; break;
                }
            }
            return isValidShot;
        }

        public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
        {
            bool isAHit = false;
            foreach (var grid in opponent.ShipLocation)
            {
                if (grid.SpotLetter == row && grid.SpotNumber == column && grid.Status == GridSpotStatus.Ship)
                {
                   isAHit = true;
                }
            }
            return isAHit;
        }

        public static void MarkShotResult(PlayerModel opponent, string row, int column, bool isAHit)
        {
            foreach (var grid in opponent.ShotGrid)
            {
                if (grid.SpotLetter == row && grid.SpotNumber == column)
                {
                    grid.Status = isAHit ? GridSpotStatus.Hit : GridSpotStatus.Miss;
                }
            }
        }
    }
}
