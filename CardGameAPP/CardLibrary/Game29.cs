using CardLibrary.Enums;
using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class Game29 : Deck
    {
        public Game29()
        {
            Create29Deck();
            ShuffleDeck();
        }

        internal void Create29Deck()
        {
            fullDeck.Clear();

            for (int suit = 0; suit < 4; suit++)
            {
                for (int val = 6; val < 13; val++)
                {
                    fullDeck.Add(new PlayingCardModel { Suite = (CardSuit)suit, Value = (CardValue)val });
                }
            }

        }
        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 8; i++)
            {
                output.Add(DrawOneCard());
            }
            return output;
        }
       
    }
}
