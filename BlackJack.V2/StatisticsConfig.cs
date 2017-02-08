namespace BlackJack.V2
{
    public struct PlayerStats
    {
        public int wins;
        public int loses;
        public int draws;
    }

    public struct GameConfig
    {
        public const int minPlayers = 2, maxPlayers = 6;
        private int _numberOfPlayers;

        public int NumberOfPlayers
        {
            get
            {
                return _numberOfPlayers;
            }
            set
            {
                if (value < minPlayers)
                {
                    _numberOfPlayers = minPlayers;
                    return;
                }
                if (value > maxPlayers)
                {
                    _numberOfPlayers = maxPlayers;
                    return;
                }
                _numberOfPlayers = value;
            }
        }
    }
}

