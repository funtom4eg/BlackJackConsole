using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.V2
{
    public static class UI
    {
        private const int _columnWidth = 13;
        private const int _screenWidth = 79;

        public static char[] suites = new char[] { (char)0x2660, (char)0x2663, (char)0x2663, (char)0x2666 };

        private static int[] _playersLastRow;
        private static int _overallLastRow;

        static UI()
        {
            Console.OutputEncoding = Encoding.Unicode;
        }

        private static string ShowCard(Card card)
        {
            if (card.Value <= CardValues.ten)
            {
                return "[ " + suites[(int)card.Suite] + " " + card.Points + " ]";
            }

            return "[ " + suites[(int)card.Suite] + " " + card.Value + " ]";
        }

        public static void Welcome(GameConfig config)
        {
            Console.WriteLine("Welcome to Black Jack game!");

            ShowConfig(config);
        }

        private static void ShowConfig(GameConfig config)
        {
            Console.WriteLine(new string('-', _screenWidth));
            Console.WriteLine("Current config:");
            Console.WriteLine("Number of players = {0}. First player is Dealer.", config.NumberOfPlayers);
            Console.WriteLine(new string('-', _screenWidth));
        }

        public static void SetConfig(ref GameConfig config)
        {
            Console.WriteLine("Press any key to use defaults or \"o\" to change config...");

            if (Console.ReadKey(true).KeyChar == 'o')
            {
                int newNumOfPlayers = config.NumberOfPlayers;

                Console.Write("Number of players (2-6 or Enter to skip): ");

                if (int.TryParse(Console.ReadLine(), out newNumOfPlayers))
                {
                    config.NumberOfPlayers = newNumOfPlayers;
                }

                ShowConfig(config);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }

            _playersLastRow = new int[config.NumberOfPlayers];
        }

        public static void ShowHeader(ref int roundNumber)
        {
            Console.Clear();
            Console.WriteLine("Round {0}. Enter Your choice (h: Hit, WhiteSpace: Stand):", ++roundNumber);
            Console.WriteLine(new string('=', _screenWidth));
        }

        public static void ShowHands(List<Player> players)
        {
            int cursorRow = Console.CursorTop;
            for (int i = 0; i < players.Count; i++)
            {
                ShowPlayerHand(players[i], i);
                Console.CursorTop = cursorRow;
            }
        }

        private static void ShowPlayerHand(Player player, int column)
        {
            int playerColumnPosition = _columnWidth * column;

            Console.CursorLeft = playerColumnPosition;

            Console.WriteLine(column == 0 ? "Dealer:" : string.Format("Player {0}:", column));

            foreach (Card card in player.Hand)
            {
                Console.CursorLeft = playerColumnPosition;
                Console.WriteLine(ShowCard(card));
            }

            _playersLastRow[column] = Console.CursorTop;

            if (_playersLastRow[column] > _overallLastRow)
            {
                _overallLastRow = _playersLastRow[column];
            }
        }

        public static void HighlightCurrentPlayer(int index)
        {
            Console.SetCursorPosition(_columnWidth * index, _playersLastRow[index]);
        }

        public static void ShowNewCard(Player player, int column)
        {
            Console.SetCursorPosition(_columnWidth * column, _playersLastRow[column]);

            Console.WriteLine(ShowCard(player.Hand[player.Hand.Count - 1]));

            _playersLastRow[column] = Console.CursorTop;

            if (_playersLastRow[column] > _overallLastRow)
            {
                _overallLastRow = _playersLastRow[column];
            }
        }

        public static char GetPlayerChoice()
        {
            char choice = default(char);

            while (choice != 'h' && choice != ' ')
            {
                choice = Console.ReadKey(true).KeyChar;
            }

            return choice;
        }

        public static void ShowResults(List<Player> players)
        {
            Console.SetCursorPosition(0, _overallLastRow);
            Console.WriteLine(new string('=', _screenWidth));
            _overallLastRow++;

            for (int i = 0; i < players.Count; i++)
            {
                Console.SetCursorPosition(_columnWidth * i, _overallLastRow);
                Console.WriteLine(players[i].Points);
            }

            _overallLastRow++;

            for (int i = 1; i < players.Count; i++)
            {
                Console.SetCursorPosition(_columnWidth * i, _overallLastRow);
                int result = players[i].LastRoundResult;
                Console.WriteLine(result > 0 ? "Win!" : result < 0 ? "Lose.." : "Draw.");
            }

            _overallLastRow++;

            for (int i = 1; i < players.Count; i++)
            {
                Console.SetCursorPosition(_columnWidth * i, _overallLastRow);
                Console.WriteLine("W - {0}", players[i].stats.wins);
                Console.CursorLeft = _columnWidth * i;
                Console.WriteLine("L - {0}", players[i].stats.loses);
                Console.CursorLeft = _columnWidth * i;
                Console.WriteLine("D - {0}", players[i].stats.draws);
            }

        }

        public static bool PromptNewRound()
        {
            Console.WriteLine(new string('=', _screenWidth));
            Console.WriteLine("Press any key to start new game, or \"x\" to exit game...");
            if (Console.ReadKey(true).KeyChar == 'x')
            {
                return false;
            }
            _overallLastRow = default(int);
            return true;
        }
    }
}

