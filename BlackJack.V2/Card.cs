using System.Collections;
using System.Collections.Generic;

namespace BlackJack.V2
{
    partial class Game
    {
        enum CardSuites { spade, club, heart, dimond }
        enum CardValues :  int
        {
            two = 2, three, four, five, six, seven, eight,
            nine, ten, Ace, Jack, Queen, King
        }

        class Card
        {
            public CardSuites Suite { get; }
            public CardValues Value { get; }
            public int Points { get; }

            public Card(CardSuites suite, CardValues value)
            {
                Suite = suite;
                Value = value;
                Points = (int)Value > 11 ? 10 : (int)Value;
            }
        }
    }
}
