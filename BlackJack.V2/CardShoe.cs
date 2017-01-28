using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    class CardShoe
    {
        readonly List<Card> sortedCardDeck = new List<Card>();

        Stack<Card> shuffledCardShoe = new Stack<Card>();

        static Random rand = new Random();

        int MinCardsInShoe { get; }

        public bool needNewShoe = false;

        public CardShoe(int numberOfDecks, double minVolumeOfShoe)
        {
            foreach (CardSuites suite in Enum.GetValues(typeof(CardSuites)))
            {
                foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
                {
                    sortedCardDeck.Add(new Card(suite, value));
                }
            }

            FillAndShuffleShoe(numberOfDecks);

            MinCardsInShoe = (int)(sortedCardDeck.Count * numberOfDecks * minVolumeOfShoe);
        }

        public void FillAndShuffleShoe(int numberOfDecks)
        {
            if (shuffledCardShoe.Count > 0)
            {
                shuffledCardShoe.Clear();
            }


            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (Card card in sortedCardDeck.OrderBy(x => rand.Next()))
                {
                    shuffledCardShoe.Push(card);
                }
            }

            //sortedCardDeck.OrderBy(x => rand.Next()).ToList().ForEach(x => shuffledCardShoe.Push(x));

        }

        public Card GiveOne()
        {
            if (shuffledCardShoe.Count < this.MinCardsInShoe)
            {
                needNewShoe = true;
            }

            return shuffledCardShoe.Pop();
        }
    }
}
