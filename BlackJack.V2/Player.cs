using System.Collections.Generic;

namespace BlackJack.V2
{
    partial class Game
    {
        class Player
        {
            public PlayerStats stats;

            public List<Card> Hand { get; }

            public Player()
            {
                Hand = new List<Card>();
                stats = new PlayerStats();
            }

            public int Points
            {
                get
                {
                    int result = 0;
                    foreach (Card card in Hand)
                    {
                        if ((int)card.Value > 11)
                        {
                            result += 10;
                            continue;
                        }

                        result += (int)card.Value;
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
