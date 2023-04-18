using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
            string letter = location.Substring(0,1);
            int number ;
            
            if ( !int.TryParse(location.Substring(1,1), out number) ||  string.IsNullOrEmpty(letter) )
            {
                return false;
            }
            GridSpotModel gridSpot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            if (!model.ShotGrid.Contains(gridSpot))
            {
                return false;
            }
           else
            {
                return true;
            }
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
           ( string letter, int number ) = SplitShotByRowAndColumn(location);
           
         
            GridSpotModel grid = new GridSpotModel();
            grid.SpotLetter = letter;
            grid.SpotNumber = number;
            grid.Status = GridSpotStatus.Ship;
            model.ShipLocation.Add(grid);
        }

        public static bool IsGameOver(PlayerModel opponent)
        {
            throw new NotImplementedException();
        }

        public static int GetShotCount(PlayerModel winner)
        {
            throw new NotImplementedException();
        }

        public static (string row, int column) SplitShotByRowAndColumn(string shot)
        {
            string row = shot.Substring(0, 1);
            int column = int.Parse(shot.Substring(1, 1));

            return (row, column);
        }

        public static bool ValidateShot(PlayerModel activePlayer, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
        {
            throw new NotImplementedException();
        }

        public static void MarkShotResult(PlayerModel opponent, string row, int column, bool isAHit)
        {
            throw new NotImplementedException();
        }
    }
}
