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
            int numberOfDecks;
            int minVolumeOfShoe;

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
            public int NumberOfDecks
            {
                get
                {
                    return numberOfDecks;
                }
                set
                {
                    if (value < 1)
                    {
                        numberOfDecks = 1;
                        return;
                    }
                    if (value > 8)
                    {
                        numberOfDecks = 8;
                        return;
                    }
                    numberOfDecks = value;
                }
            }
            public int MinVolumeOfShoe
            {
                get
                {
                    return minVolumeOfShoe;
                }
                set
                {
                    if (value < 30)
                    {
                        minVolumeOfShoe = 30;
                        return;
                    }
                    if (value > 100)
                    {
                        minVolumeOfShoe = 100;
                        return;
                    }
                    minVolumeOfShoe = value;
                }
            }
        }
    }
}
