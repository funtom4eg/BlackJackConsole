using System.Collections.Generic;

namespace BlackJack.V2
{
    partial class Game
    {
        class Player
        {
            public List<Card> Hand
            {
                get;
            }

            public Player()
            {
                Hand = new List<Card>();
            }

            public int Points
            {
                get
                {
                    int result = 0;
                    foreach (Card card in Hand)
                    {
                        if (card.Points > 11)
                        {
                            result += 10;
                            continue;
                        }

                        result += card.Points;
                    }

                    return result;
                }
            }

            public void TakeOne(Card card)
            {
                Hand.Add(card);
            }

            public void Clear()
            {
                Hand.Clear();
            }
        }
    }
}
