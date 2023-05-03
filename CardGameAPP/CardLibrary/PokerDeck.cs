using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class PokerDeck : Deck
    {
        public PokerDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }
        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 5; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardToDiscard) 
        { 
            List<PlayingCardModel> output = new List<PlayingCardModel> ();

            foreach (var card in cardToDiscard)
            {
                output.Add(DrawOneCard());
                discardPile.Add(card);
            }
            return output;
        }
    }

}
