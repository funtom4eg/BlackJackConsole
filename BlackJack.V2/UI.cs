using System;
using System.Text;

namespace BlackJack.V2
{
    partial class Game
    {
        static class UI
        {
            static int[] playersLastRow;
            static int overallLastRow;

            public const int columnWidth = 15;
            public const int screenWidth = 90;
            static UI()
            {
                Console.OutputEncoding = Encoding.Unicode;
            }

            static string ShowCard(Card card)
            {

                if (card.Value <= CardValues.ten)
                {
                    return "[ " + (char)card.Suite + " " + (int)card.Value + " ]";
                }

                return "[ " + (char)card.Suite + " " + card.Value + " ]";
            }

            public static void Welcome(ref GameConfig config)
            {
                Console.WriteLine("Welcome to Black Jack game!");

                ShowConfig(ref config);

                Console.WriteLine("Press any key to use defaults or \"o\" to change config...");

                if (Console.ReadKey(true).KeyChar == 'o')
                {
                    SetConfig(ref config);
                }

                playersLastRow = new int[config.NumberOfPlayers];

                Console.Clear();
            }

            static void ShowConfig(ref GameConfig config)
            {
                Console.WriteLine(new string('-', screenWidth));
                Console.WriteLine("Current config:");
                Console.WriteLine("Number of players = {0}", config.NumberOfPlayers);
                Console.WriteLine("Number of decks = {0}", config.NumberOfDecks);
                Console.WriteLine("Minimum volume of shoe before reset = {0} %", config.MinVolumeOfShoe);
                Console.WriteLine(new string('-', screenWidth));
            }

            static void SetConfig(ref GameConfig config)
            {
                int newNumOfPlayers = config.NumberOfPlayers;
                int newNumOfDecks = config.NumberOfDecks;
                int newMinVolumeOfShoe = config.MinVolumeOfShoe;

                Console.Write("Number of players (2-6 or Enter to skip): ");
                if (int.TryParse(Console.ReadLine(), out newNumOfPlayers))  //TODO: Question: How can I do it without else?
                {
                    config.NumberOfPlayers = newNumOfPlayers;
                }
                else
                {
                    Console.WriteLine("Incorrect value, no changes.");
                }

                Console.Write("Number of decks in shoe (1-8 or Enter to skip): ");
                if (int.TryParse(Console.ReadLine(), out newNumOfDecks))
                {
                    config.NumberOfDecks = newNumOfDecks;
                }

                Console.Write("Minimum volume of shoe before reset in percent (30-100 or Enter to skip): ");
                if (int.TryParse(Console.ReadLine(), out newMinVolumeOfShoe))
                {
                    config.MinVolumeOfShoe = newMinVolumeOfShoe;
                }

                ShowConfig(ref config);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
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

            static void ShowPlayerHand(Player player, int column)
            {
                int playerColumnPosition = columnWidth * column;

                Console.CursorLeft = playerColumnPosition;

                if (column == 0)
                {
                    Console.WriteLine("Dealer:");
                }
                else                                                     //TODO: Question: Another way?
                {
                    Console.WriteLine("Player {0}:", column);
                }

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
                    if (players[i].Points > 21)
                    {
                        Console.WriteLine("Lose..");
                        players[i].stats.loses++;
                        continue;
                    }
                    if (players[0].Points > 21 || players[0].Points < players[i].Points)
                    {
                        Console.WriteLine("Win!");
                        players[i].stats.wins++;
                        continue;
                    }
                    if (players[0].Points > players[i].Points)
                    {
                        Console.WriteLine("Lose...");
                        players[i].stats.loses++;
                        continue;
                    }
                    Console.WriteLine("Draw.");
                    players[i].stats.draws++;
                }

                overallLastRow++;

                for (int i = 1; i < players.Length; i++)
                {
                    Console.SetCursorPosition(columnWidth * i, overallLastRow);
                    Console.WriteLine("W-{0}  L-{1}", players[i].stats.wins, players[i].stats.loses);
                    Console.CursorLeft = columnWidth * i;
                    Console.WriteLine("Draws - {0}", players[i].stats.draws);
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
                overallLastRow = 0;
                return true;
            }
        }
    }
}
