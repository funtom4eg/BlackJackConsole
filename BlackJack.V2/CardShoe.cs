using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    partial class Game
    {
        class CardDeck
        {
            readonly List<Card> blankDeck = new List<Card>();
            Stack<Card> shuffledCardDeck = new Stack<Card>();

            static Random rand = new Random();

            public CardDeck()
            {
                CreateBlankDeck();

                FillAndShuffleDeck();
            }

            void CreateBlankDeck() //TODO: redo? it works good =)
            {
                foreach (CardSuites suite in Enum.GetValues(typeof(CardSuites)))
                {
                    foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
                    {
                        blankDeck.Add(new Card(suite, value));
                    }
                }
            }

            public void FillAndShuffleDeck()
            {
                if (shuffledCardDeck.Count > 0)
                {
                    shuffledCardDeck.Clear();
                }

                foreach (Card card in blankDeck.OrderBy(x => rand.Next()))
                {
                    shuffledCardDeck.Push(card);
                }
            }

            public Card GiveOne()
            {
                return shuffledCardDeck.Pop();
            }
        }
    }
}
