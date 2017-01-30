using System.Linq;
namespace BlackJack.V2
{
    partial class Game
    {
        int roundNumber;
        GameConfig config;

        CardShoe cardShoe;
        Player[] players;

        bool startNewRound = true;

        public Game()
        {

            config = new GameConfig() { NumberOfPlayers = 2, NumberOfDecks = 1, MinVolumeOfShoe = 50 };

            UI.Welcome(ref config);

            cardShoe = new CardShoe(config.NumberOfDecks, config.MinVolumeOfShoe);

            players = new Player[config.NumberOfPlayers];
            for (int i = 0; i < config.NumberOfPlayers; i++)
            {
                players[i] = new Player();
            }

            while (startNewRound == true)
            {
                if (cardShoe.needShoeReset)
                {
                    cardShoe.FillAndShuffleShoe(config.NumberOfDecks);
                }
                
                foreach (Player player in players)
                {
                    player.Clear();
                }

                StartNewRound();
            }
        }

        void StartNewRound()
        {
            UI.ShowHeader(ref roundNumber);

            FirstSet();

            PlayersTurn();

            DealersTurn();

            UI.ShowResults(players);

            startNewRound = UI.PromptNewRound();
        }

        void FirstSet()
        {
            players[0].TakeOne(cardShoe.GiveOne());

            for (int i = 1; i < config.NumberOfPlayers; i++)
            {
                players[i].TakeOne(cardShoe.GiveOne());
                players[i].TakeOne(cardShoe.GiveOne());
            }

            UI.ShowHands(players);
        }

        void PlayersTurn()
        {
            char playersChoice = default(char);
            for (int i = 1; i < players.Length; i++)
            {
                UI.HighlightCurrentPlayer(i);
                while (players[i].Points < 21)
                {
                    playersChoice = UI.GetPlayerChoice();

                    if (playersChoice == ' ')
                    {
                        break;
                    }

                    players[i].TakeOne(cardShoe.GiveOne());
                    UI.ShowNewCard(players[i], i);
                    UI.HighlightCurrentPlayer(i);
                }
            }
        }

        void DealersTurn()
        {
            if (players.Where(x => x.Points <= 21).Count() > 1)
            {
                while (players[0].Points < 17)
                {
                    players[0].TakeOne(cardShoe.GiveOne());
                    UI.ShowNewCard(players[0], 0);
                }
            }
        }
    }
}
