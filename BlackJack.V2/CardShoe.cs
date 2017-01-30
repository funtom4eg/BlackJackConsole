using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    partial class Game
    {
        class CardShoe
        {
            readonly List<Card> blankDeck = new List<Card>();
            Stack<Card> shuffledCardShoe = new Stack<Card>();

            static Random rand = new Random();

            int minCardsInShoe;
            int cardsInShoe
            {
                get
                {
                    return shuffledCardShoe.Count;
                }
            }

            public bool needShoeReset = false;

            public CardShoe(int numberOfDecks, int minVolumeOfShoe)
            {
                CreateBlankDeck();

                FillAndShuffleShoe(numberOfDecks);

                minCardsInShoe = blankDeck.Count * numberOfDecks * minVolumeOfShoe / 100;
            }

            void CreateBlankDeck()
            {
                foreach (CardSuites suite in Enum.GetValues(typeof(CardSuites)))
                {
                    foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
                    {
                        blankDeck.Add(new Card(suite, value));
                    }
                }
            }

            public void FillAndShuffleShoe(int numberOfDecks)
            {
                if (shuffledCardShoe.Count > 0)
                {
                    shuffledCardShoe.Clear();
                }

                List<Card> multiDeck = new List<Card>();

                for (int i = 0; i < numberOfDecks; i++)
                {
                    multiDeck.AddRange(blankDeck);
                }

                foreach (Card card in multiDeck.OrderBy(x => rand.Next()))
                {
                    shuffledCardShoe.Push(card);
                }
            }

            public Card GiveOne()
            {
                if (cardsInShoe < minCardsInShoe)
                {
                    needShoeReset = true;
                }

                return shuffledCardShoe.Pop();
            }
        }
    }
}
