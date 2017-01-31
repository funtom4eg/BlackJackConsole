namespace BlackJack.V2
{
    partial class Game
    {
        struct PlayerStats
        {
            public int wins;
            public int loses;
            public int draws;
        }

        struct GameConfig
        {
            int numberOfPlayers;
           
            public int NumberOfPlayers
            {
                get
                {
                    return numberOfPlayers;
                }
                set
                {
                    if (value < 2)
                    {
                        numberOfPlayers = 2;
                        return;
                    }
                    if (value > 6)
                    {
                        numberOfPlayers = 6;
                        return;
                    }
                    numberOfPlayers = value;
                }
            }
        }
    }
}
