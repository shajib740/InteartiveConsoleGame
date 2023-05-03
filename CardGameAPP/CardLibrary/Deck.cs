using CardLibrary.Enums;
using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public abstract class Deck
    {
        protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
        protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
        protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();

        internal void CreateDeck()
        {
            fullDeck.Clear();

            for (int suit = 0; suit < 4; suit++)
            {
                for (int val = 0; val < 13; val++)
                {
                    fullDeck.Add(new PlayingCardModel { Suite = (CardSuit)suit, Value = (CardValue)val });
                }
            }

        }
        public virtual void ShuffleDeck()
        {
            var rand = new Random();
            drawPile = fullDeck.OrderBy(x => rand.Next()).ToList();
        }

        public abstract List<PlayingCardModel> DealCards();

        protected virtual PlayingCardModel DrawOneCard()
        {
            PlayingCardModel output = drawPile.Take(1).First();
            drawPile.Remove(output);

            return output;
        }
    }

}
