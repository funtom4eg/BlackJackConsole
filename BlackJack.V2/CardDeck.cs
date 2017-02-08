using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    public class CardDeck
    {
        private readonly List<Card> _newCardDeck = new List<Card>();
        private Stack<Card> _shuffledCardDeck = new Stack<Card>();

        private static Random _rand = new Random();

        public CardDeck()
        {
            CreateNewDeck();

            FillAndShuffleDeck();
        }

        private void CreateNewDeck()
        {
            foreach (CardSuites suite in Enum.GetValues(typeof(CardSuites)))
            {
                int points = 2;
                for (var value = CardValues.two; value <= CardValues.Ace; value++)
                {
                    Card card = new Card();
                    card.Suite = suite;
                    card.Value = value;
                    card.Points = points++;
                    _newCardDeck.Add(card);
                }

                points = 10;
                for (var value = CardValues.Jack; value <= CardValues.King; value++)
                {
                    Card card = new Card();
                    card.Suite = suite;
                    card.Value = value;
                    card.Points = points;
                    _newCardDeck.Add(card);
                }
            }
        }

        public void FillAndShuffleDeck()
        {
            if (_shuffledCardDeck.Count > 0)
            {
                _shuffledCardDeck.Clear();
            }

            foreach (Card card in _newCardDeck.OrderBy(x => Guid.NewGuid()))    //_rand.Next())) ?
            {
                _shuffledCardDeck.Push(card);
            }
        }

        public Card GiveOne()
        {
            return _shuffledCardDeck.Pop();
        }
    }
}

