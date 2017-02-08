using System.Collections;
using System.Collections.Generic;

namespace BlackJack.V2
{
    public class Card
    {
        public CardSuites Suite { get; set; }
        public CardValues Value { get; set; }
        public int Points { get; set; }
    }
}

