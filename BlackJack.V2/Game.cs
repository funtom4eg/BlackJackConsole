using System.Collections.Generic;
using System.Linq;
namespace BlackJack.V2
{
    public class Game
    {
        private int _roundNumber;
        private GameConfig _config;

        private CardDeck _cardDeck;
        private List<Player> _players;

        private int _blackJack = 21;
        private int _minDealerPoints = 17;

        private bool _startNewRound = true;

        public Game()
        {
            _config = new GameConfig() { NumberOfPlayers = GameConfig.minPlayers };

            UI.Welcome(_config);

            UI.SetConfig(ref _config);

            _cardDeck = new CardDeck();

            _players = new List<Player>(_config.NumberOfPlayers);
            for (int i = 0; i < _config.NumberOfPlayers; i++)
            {
                _players.Add(new Player());
            }

            while (_startNewRound == true)
            {
                StartNewRound();
            }
        }

        private void StartNewRound()
        {
            _cardDeck.FillAndShuffleDeck();

            UI.ShowHeader(ref _roundNumber);

            FirstSet();

            PlayersTurn();

            DealersTurn();

            SetResults();

            UI.ShowResults(_players);

            _startNewRound = UI.PromptNewRound();

            ClearHands();
        }

        private void FirstSet()
        {
            _players[0].TakeOne(_cardDeck.GiveOne());

            for (int i = 1; i < _config.NumberOfPlayers; i++)
            {
                _players[i].TakeOne(_cardDeck.GiveOne());
                _players[i].TakeOne(_cardDeck.GiveOne());
            }

            UI.ShowHands(_players);
        }

        private void PlayersTurn()
        {
            char playersChoice = default(char);
            for (int i = 1; i < _players.Count; i++)
            {
                UI.HighlightCurrentPlayer(i);
                while (_players[i].Points < _blackJack)
                {
                    playersChoice = UI.GetPlayerChoice();

                    if (playersChoice == ' ')
                    {
                        break;
                    }

                    _players[i].TakeOne(_cardDeck.GiveOne());
                    UI.ShowNewCard(_players[i], i);
                    UI.HighlightCurrentPlayer(i);
                }
            }
        }

        private void DealersTurn()
        {
            if (_players.Where(x => x.Points <= _blackJack).Count() > 1)
            {
                while (_players[0].Points < _minDealerPoints)
                {
                    _players[0].TakeOne(_cardDeck.GiveOne());
                    UI.ShowNewCard(_players[0], 0);
                }
            }
        }

        private void SetResults()
        {
            for (int i = 1; i < _players.Count; i++)
            {
                if (_players[i].Points > _blackJack)
                {
                    _players[i].LastRoundResult = -1;
                    _players[i].stats.loses++;
                    continue;
                }
                if (_players[0].Points > _blackJack || _players[0].Points < _players[i].Points)
                {
                    _players[i].LastRoundResult = 1;
                    _players[i].stats.wins++;
                    continue;
                }
                if (_players[0].Points > _players[i].Points)
                {
                    _players[i].LastRoundResult = -1;
                    _players[i].stats.loses++;
                    continue;
                }
                _players[i].LastRoundResult = 0;
                _players[i].stats.draws++;
            }
        }

        private void ClearHands()
        {
            foreach (Player player in _players)
            {
                player.Clear();
            }
        }
    }
}
