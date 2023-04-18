using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                // bool isValidShot = isValidShot()
                // Determine the result
                // Determine  if the game is over
                // else, go to opponent (swap position)


            } while (winner == null);

            Console.ReadLine();
        }

        private static void DisplayshotGrid(PlayerModel activePlayer)
        {
            foreach (var gridspot in activePlayer.ShotGrid)
            {
                Console.Write($"Shot location {gridspot.SpotLetter}/ {gridspot.SpotNumber} / {gridspot.Status} ");
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
          //  BattleshipLogic.InitializeShotGrid(output);

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
           
        }
    }
}
