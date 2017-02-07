using System.Linq;
namespace BlackJack.V2
{
    class Game
    {
        private int roundNumber;
        private GameConfig config;

        private CardDeck cardDeck;
        private Player[] players;

        private bool startNewRound = true;

        public Game()
        {
            config = new GameConfig() { NumberOfPlayers = GameConfig.minPlayers };

            UI.Welcome(config);

            UI.SetConfig(ref config);

            cardDeck = new CardDeck();

            players = new Player[config.NumberOfPlayers];
            for (int i = 0; i < config.NumberOfPlayers; i++)
            {
                players[i] = new Player();
            }

            while (startNewRound == true)
            {
                StartNewRound();
            }
        }

        private void StartNewRound()
        {
            cardDeck.FillAndShuffleDeck();

            UI.ShowHeader(ref roundNumber);

            FirstSet();

            PlayersTurn();

            DealersTurn();

            SetResults();

            UI.ShowResults(players);

            startNewRound = UI.PromptNewRound();

            ClearHands();
        }

        private void FirstSet()
        {
            players[0].TakeOne(cardDeck.GiveOne());

            for (int i = 1; i < config.NumberOfPlayers; i++)
            {
                players[i].TakeOne(cardDeck.GiveOne());
                players[i].TakeOne(cardDeck.GiveOne());
            }

            UI.ShowHands(players);
        }

        private void PlayersTurn()
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

                    players[i].TakeOne(cardDeck.GiveOne());
                    UI.ShowNewCard(players[i], i);
                    UI.HighlightCurrentPlayer(i);
                }
            }
        }

        private void DealersTurn()
        {
            if (players.Where(x => x.Points <= 21).Count() > 1)
            {
                while (players[0].Points < 17)
                {
                    players[0].TakeOne(cardDeck.GiveOne());
                    UI.ShowNewCard(players[0], 0);
                }
            }
        }

        private void SetResults()
        {
            for (int i = 1; i < players.Length; i++)
            {
                if (players[i].Points > 21)
                {
                    players[i].LastRoundResult = -1;
                    players[i].stats.loses++;
                    continue;
                }
                if (players[0].Points > 21 || players[0].Points < players[i].Points)
                {
                    players[i].LastRoundResult = 1;
                    players[i].stats.wins++;
                    continue;
                }
                if (players[0].Points > players[i].Points)
                {
                    players[i].LastRoundResult = -1;
                    players[i].stats.loses++;
                    continue;
                }
                players[i].LastRoundResult = 0;
                players[i].stats.draws++;
            }
        }

        private void ClearHands()
        {
            foreach (Player player in players)
            {
                player.Clear();
            }
        }
    }
}
