using CardLibrary;
using CardLibrary.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //    PokerDeck deck = new PokerDeck();

            //    var hand = deck.DealCards();

            //    foreach (var card in hand)
            //    {
            //        Console.WriteLine($"{card.Suite} {card.Value}");
            //    }

            //BlackJack deck = new BlackJack();

            //var hand = deck.DealCards();

            //foreach (var card in hand)
            //{
            //    Console.WriteLine($"{card.Suite} {card.Value}");
            //}

            Game29 game = new Game29();
            var hand = game.DealCards();

            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Value} of {card.Suite}");
            }


            Console.ReadLine();
        }
    }

}
