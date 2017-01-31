using System.Collections;
using System.Collections.Generic;

namespace BlackJack.V2
{
    partial class Game
    {
        enum CardSuites { spade = 0x2660, club = 0x2663, heart = 0x2665, dimond = 0x2666 } //TODO: codes -> UI
        enum CardValues //TODO: correct
        {
            two = 2, three, four, five, six = 6, seven = 7, eight = 8,
            nine = 9, ten = 10, Jack = 10, Queen = 10, King = 10, Ace = 11
        }

        class Card //TODO: add points
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
