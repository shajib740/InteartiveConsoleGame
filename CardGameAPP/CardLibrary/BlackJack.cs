using CardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class BlackJack : Deck
    {
        public BlackJack()
        {
            CreateDeck();
            ShuffleDeck();
        }

        public override List<PlayingCardModel> DealCards()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            for (int i = 0; i < 2; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public PlayingCardModel RequestCard()
        {
            return DrawOneCard();
        }
    }
}
