using System.Collections;
using System.Collections.Generic;

namespace BlackJack.V2
{
    partial class Game
    {
        enum CardSuites { spade = 0x2660, club = 0x2663, heart = 0x2665, dimond = 0x2666 }
        enum CardValues
        {
            two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8,
            nine = 9, ten = 10, Jack = 20, Queen = 30, King = 40, Ace = 11
        }

        class Card
        {
            public CardSuites Suite { get; }
            public CardValues Value { get; }

            public Card(CardSuites suite, CardValues value)
            {
                Suite = suite;
                Value = value;
            }
        }
    }
}
