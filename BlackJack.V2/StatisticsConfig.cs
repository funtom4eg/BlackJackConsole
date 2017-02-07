namespace BlackJack.V2
{
    struct PlayerStats
    {
        public int wins;
        public int loses;
        public int draws;
    }

    struct GameConfig
    {
        private int numberOfPlayers;
        public const int minPlayers = 2, maxPlayers = 6;

        public int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                if (value < minPlayers)
                {
                    numberOfPlayers = minPlayers;
                    return;
                }
                if (value > maxPlayers)
                {
                    numberOfPlayers = maxPlayers;
                    return;
                }
                numberOfPlayers = value;
            }
        }
    }
}

