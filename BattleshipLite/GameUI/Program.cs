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
            PlayerModel activePlayer = CreatePlayer("Player 1",false);
            PlayerModel opponent = CreatePlayer("Player 2",true);

            PlayerModel winner = null;

            do
            {
                Console.WriteLine($"Shot Grid of {opponent.UserName}");
                DisplayshotGrid(opponent);
               
                
                RecordPlayerShot(activePlayer, opponent);
               

                Console.WriteLine($"Shot Grid of {opponent.UserName}");
                DisplayshotGrid(opponent);
               
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
                Console.WriteLine();
            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UserName}");
            Console.WriteLine($"{winner.UserName} took {BattleshipLogic.GetShotCount(winner)} shots.");
        }

        private static void RecordPlayerShot(PlayerModel activePlayer, PlayerModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;
            do
            {
                string shot = AskforShot(activePlayer.UserName);
                try
                {
                   
                    (row, column) = BattleshipLogic.SplitShotByRowAndColumn(shot);
                    isValidShot = BattleshipLogic.ValidateShot(opponent, row, column);
                }
                catch (Exception ex)
                {
                    isValidShot = false;
                }

               
                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again");
                }

            } while (isValidShot == false);


            // Determine shot results (hit/miss)
            bool isAHit = BattleshipLogic.IdentifyShotResult(opponent, row, column);

           
            BattleshipLogic.MarkShotResult(opponent, row, column, isAHit);
            DisplayShotResults(row, column, isAHit);
            Console.WriteLine();
        }

        private static void DisplayShotResults(string row, int column, bool isAHit)
        {
            if (isAHit)
            {
                Console.WriteLine($" {row}{column} is a Hit");
            }
            else
            {
                Console.WriteLine($" {row}{column} is a miss");
            }
            Console.WriteLine();
        }

        private static string AskforShot(string userName)
        {

            Console.WriteLine($"Please enter your shot selection {userName}");
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
                if (gridspot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($"{gridspot.SpotLetter}{gridspot.SpotNumber} ");
                }
                else if (gridspot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridspot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship App");
            Console.WriteLine("Created by Shajib");
            Console.WriteLine();
        }
        private static PlayerModel CreatePlayer(string playerTitle,bool isRandomGenerate)
        {

            PlayerModel output = new PlayerModel();
            Console.WriteLine($"Player information for {playerTitle}");
         
            output.UserName = AskForPlayerName();
           
            BattleshipLogic.InitializeShotGrid(output);
            if (isRandomGenerate)
            {
                BattleshipLogic.PlaceShipRandomly(output);
            }
            else
            {
                PlaceShip(output);
            }

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

                bool isValidLocation = false; ;  // Check and store the ship location

                try
                {
                    isValidLocation = BattleshipLogic.PlaceShip(model, location);  // Check and store the ship location
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: "+ ex.Message);
                    isValidLocation = false;
                }
                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please add again.");
                }

            } while (model.ShipLocation.Count < 5);
        }
    }
}
