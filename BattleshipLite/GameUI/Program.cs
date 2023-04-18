using BattleshipLibrary.Models;
using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            PlayerModel activePlayer = CreatePlayer("Player 1");
            PlayerModel opponent = CreatePlayer("Player 2");

            PlayerModel winner = null;

            do
            {
                // Display grid for activePlayer  on where they fired
                DisplayshotGrid(activePlayer);


                // Ask player  for a shot
                Console.WriteLine("Please fire a shot");
                string shotFired = Console.ReadLine();
                // Determine if it is a valid shot
                // Determine the result

                RecordPlayerShot(activePlayer, opponent);


                // Determine  if the game should continue

                bool isGameOver = BattleshipLogic.IsGameOver(opponent);

                if (isGameOver == true)
                {
                    winner = activePlayer;
                }
                else
                {
                    // Swap using a temp variable
                    //PlayerModel tempPlayer = opponent;
                    //opponent = activePlayer;
                    //activePlayer = tempPlayer;

                    // Swap using tuple
                    (activePlayer, opponent) = (opponent, activePlayer);
                }

            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UserName}");
            Console.WriteLine($"{winner.UserName} tool {BattleshipLogic.GetShotCount(winner)} shots.");
        }

        private static void RecordPlayerShot(PlayerModel activePlayer, PlayerModel opponent)
        {
            bool isValidShot = false;
            string row;
            int column;
            do
            {
                string shot = AskforShot();
                ( row,  column) = BattleshipLogic.SplitShotByRowAndColumn(shot);

                isValidShot = BattleshipLogic.ValidateShot(activePlayer, row, column);
                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again");
                }

            } while (isValidShot == false);


            // Determine shot results (hit/miss)
            bool isAHit = BattleshipLogic.IdentifyShotResult(opponent, row, column);

            // Record results
            BattleshipLogic.MarkShotResult(opponent, row, column, isAHit);
        }

        private static string AskforShot()
        {
            Console.WriteLine("Please enter your shot selection");
            string output = Console.ReadLine();
            return output;  
        }

        private static void DisplayshotGrid(PlayerModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridspot in activePlayer.ShotGrid)
            {
                if (currentRow != gridspot.SpotLetter)
                {
                    currentRow = gridspot.SpotLetter;
                    Console.WriteLine();
                }
                if (gridspot.Status != GridSpotStatus.Empty)
                {
                    Console.Write($"Shot location {gridspot.SpotLetter}{gridspot.SpotNumber} ");
                }
                else if (gridspot.Status == GridSpotStatus.Hit)
                {
                    Console.WriteLine(" X ");
                }
                else if (gridspot.Status == GridSpotStatus.Miss)
                {
                    Console.WriteLine(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }


            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship App");
            Console.WriteLine("Created by Shajib");
            Console.WriteLine();
        }
        private static PlayerModel CreatePlayer(string playerTitle)
        {

            PlayerModel output = new PlayerModel();
            Console.WriteLine($"Player information for {playerTitle}");
            // ask player for their name
            output.UserName = AskForPlayerName();
            // Load up the shot grid
            BattleshipLogic.InitializeShotGrid(output);

            // Ask the user for their 5 ship placements
            PlaceShip(output);
            // clear
            Console.Clear();

            return output;
        }
        private static string AskForPlayerName()
        {
            Console.Write("Enter player name:");
            string output = Console.ReadLine();

            return output;
        }
        private static void PlaceShip(PlayerModel model)
        {
            do
            {
                Console.Write($"Where do you want to place ship number {model.ShipLocation.Count + 1}:");
                string location = Console.ReadLine();

                bool isValidLocation = BattleshipLogic.PlaceShip(model, location);
                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please add again.");
                }
                else
                {
                    BattleshipLogic.AddShipPlcae(model, location, GridSpotStatus.Ship);
                }

            } while (model.ShipLocation.Count < 5);
        }
    }
}
