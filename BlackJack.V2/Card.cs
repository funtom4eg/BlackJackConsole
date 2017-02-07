using System.Collections;
using System.Collections.Generic;

namespace BlackJack.V2
{
    enum CardSuites { spade, club, heart, dimond }
    enum CardValues
    {
        two = 2, three, four, five, six, seven, eight,
        nine, ten, Ace, Jack, Queen, King
    }

    class Card
    {
        public CardSuites Suite { get; }
        public CardValues Value { get; }
        public int Points { get; }

        public Card(CardSuites suite, CardValues value, int pts)
        {
            Suite = suite;
            Value = value;
            Points = pts;
        }
    }
}

