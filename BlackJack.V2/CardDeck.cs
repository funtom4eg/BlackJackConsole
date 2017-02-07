using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    class CardDeck
    {
        private readonly List<Card> newCardDeck = new List<Card>();
        private Stack<Card> shuffledCardDeck = new Stack<Card>();

        private static Random rand = new Random();

        public CardDeck()
        {
            CreateNewDeck();

            FillAndShuffleDeck();
        }

        private void CreateNewDeck()
        {
            foreach (CardSuites suite in Enum.GetValues(typeof(CardSuites)))
            {
                for (int i = (int)CardValues.two; i <= (int)CardValues.Ace; i++)
                {
                    newCardDeck.Add(new Card(suite, (CardValues)i, i));
                }

                for (int i = (int)CardValues.Jack; i <= (int)CardValues.King; i++)
                {
                    newCardDeck.Add(new Card(suite, (CardValues)i, (int)CardValues.ten));
                }
            }
        }

        public void FillAndShuffleDeck()
        {
            if (shuffledCardDeck.Count > 0)
            {
                shuffledCardDeck.Clear();
            }

            foreach (Card card in newCardDeck.OrderBy(x => rand.Next()))
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

