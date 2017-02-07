using System;
using System.Text;

namespace BlackJack.V2
{
    static class UI
    {
        public const int columnWidth = 13;
        public const int screenWidth = 79;

        public static char[] suites = new char[] { (char)0x2660, (char)0x2663, (char)0x2663, (char)0x2666 };

        private static int[] playersLastRow;
        private static int overallLastRow;

        static UI()
        {
            Console.OutputEncoding = Encoding.Unicode;
        }

        private static string ShowCard(Card card)
        {
            if (card.Value <= CardValues.ten)
            {
                return "[ " + suites[(int)card.Suite] + " " + (int)card.Value + " ]";
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
            Console.WriteLine(new string('-', screenWidth));
            Console.WriteLine("Current config:");
            Console.WriteLine("Number of players = {0}. First player is Dealer.", config.NumberOfPlayers);
            Console.WriteLine(new string('-', screenWidth));
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

            playersLastRow = new int[config.NumberOfPlayers];
        }

        public static void ShowHeader(ref int roundNumber)
        {
            Console.Clear();
            Console.WriteLine("Round {0}. Enter Your choice (h: Hit, WhiteSpace: Stand):", ++roundNumber);
            Console.WriteLine(new string('=', screenWidth));
        }

        public static void ShowHands(Player[] players)
        {
            int cursorRow = Console.CursorTop;
            for (int i = 0; i < players.Length; i++)
            {
                ShowPlayerHand(players[i], i);
                Console.CursorTop = cursorRow;
            }
        }

        private static void ShowPlayerHand(Player player, int column)
        {
            int playerColumnPosition = columnWidth * column;

            Console.CursorLeft = playerColumnPosition;

            Console.WriteLine(column == 0 ? "Dealer:" : string.Format("Player {0}:", column));

            foreach (Card card in player.Hand)
            {
                Console.CursorLeft = playerColumnPosition;
                Console.WriteLine(ShowCard(card));
            }

            playersLastRow[column] = Console.CursorTop;

            if (playersLastRow[column] > overallLastRow)
            {
                overallLastRow = playersLastRow[column];
            }
        }

        public static void HighlightCurrentPlayer(int index)
        {
            Console.SetCursorPosition(columnWidth * index, playersLastRow[index]);
        }

        public static void ShowNewCard(Player player, int column)
        {
            Console.SetCursorPosition(columnWidth * column, playersLastRow[column]);

            Console.WriteLine(ShowCard(player.Hand[player.Hand.Count - 1]));

            playersLastRow[column] = Console.CursorTop;

            if (playersLastRow[column] > overallLastRow)
            {
                overallLastRow = playersLastRow[column];
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

        public static void ShowResults(Player[] players)
        {
            Console.SetCursorPosition(0, overallLastRow);
            Console.WriteLine(new string('=', screenWidth));
            overallLastRow++;

            for (int i = 0; i < players.Length; i++)
            {
                Console.SetCursorPosition(columnWidth * i, overallLastRow);
                Console.WriteLine(players[i].Points);
            }

            overallLastRow++;

            for (int i = 1; i < players.Length; i++)
            {
                Console.SetCursorPosition(columnWidth * i, overallLastRow);
                int result = players[i].LastRoundResult;
                Console.WriteLine(result > 0 ? "Win!" : result < 0 ? "Lose.." : "Draw.");
            }

            overallLastRow++;

            for (int i = 1; i < players.Length; i++)
            {
                Console.SetCursorPosition(columnWidth * i, overallLastRow);
                Console.WriteLine("W - {0}", players[i].stats.wins);
                Console.CursorLeft = columnWidth * i;
                Console.WriteLine("L - {0}", players[i].stats.loses);
                Console.CursorLeft = columnWidth * i;
                Console.WriteLine("D - {0}", players[i].stats.draws);
            }

        }

        public static bool PromptNewRound()
        {
            Console.WriteLine(new string('=', screenWidth));
            Console.WriteLine("Press any key to start new game, or \"x\" to exit game...");
            if (Console.ReadKey(true).KeyChar == 'x')
            {
                return false;
            }
            overallLastRow = default(int);
            return true;
        }
    }
}

