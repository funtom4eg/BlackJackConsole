namespace BlackJack.V2
{
    class Game
    {
        GameStatistics stats;

        CardShoe cardShoe;

        Player dealer;
        Player player;

        bool startNewRound = true;

        public Game(int numOfPlayers = 1, int numberOfDecks = 1, double minVolumeOfShoe = 0.5)
        {
            stats = new GameStatistics();

            UI.Greetings();

            cardShoe = new CardShoe(numberOfDecks, minVolumeOfShoe);

            dealer = new Player();
            player = new Player();

            while (startNewRound)
            {
                NewRound();

                if (cardShoe.needNewShoe)
                {
                    cardShoe.FillAndShuffleShoe(numberOfDecks);
                }

                dealer.Clear();
                player.Clear();
            }
        }

        void NewRound()
        {
            UI.ShowHeader(ref stats);

            FirstSet();

            PlayersTurn();

            DealersTurn();

            UI.ShowResults(ref stats, dealer.Points, player.Points);

            startNewRound = UI.PromptNewRound();
        }

        void FirstSet()
        {
            dealer.TakeOne(cardShoe.GiveOne());

            player.TakeOne(cardShoe.GiveOne());
            player.TakeOne(cardShoe.GiveOne());

            UI.ShowHands(dealer, player);
        }

        void PlayersTurn()
        {
            char playersChoice = default(char);
            while (player.Points < 21)
            {
                playersChoice = UI.GetPlayerChoice();

                if (playersChoice == ' ')
                {
                    break;
                }

                player.TakeOne(cardShoe.GiveOne());
                UI.ShowHands(dealer, player);
            }
        }

        void DealersTurn()
        {
            if (player.Points <= 21)
            {
                while (dealer.Points < 17)
                    dealer.TakeOne(cardShoe.GiveOne());
                UI.ShowHands(dealer, player);
            }
        }
    }
}
