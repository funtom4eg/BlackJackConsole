using System.Collections.Generic;
using System.Linq;

namespace BlackJack.V2
{
    class Player
    {
        public PlayerStats stats;

        public List<Card> Hand { get; }

        public int LastRoundResult { get; set; }

        public Player()
        {
            Hand = new List<Card>();
            stats = new PlayerStats();
        }

        public int Points
        {
            get
            {
                return Hand.Sum(x => x.Points);
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

