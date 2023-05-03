using CardLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary.Models
{
    public class PlayingCardModel
    {
        public CardSuit Suite { get; set; }
        public CardValue Value { get; set; }
    }


}
