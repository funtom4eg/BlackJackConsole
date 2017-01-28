using System;
using System.Text;

namespace BlackJack.V2
{
    static class UI
    {
        static UI()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
        }

        static string DisplayCard(Card card)
        {

            if (card.Value <= CardValues.ten)
            {
                return "[ " + (char)card.Suite + " " + card.Points + " ]";
            }

            return "[ " + (char)card.Suite + " " + card.Value + " ]";
        }

        public static void Greetings()
        {
            Console.WriteLine("Welcome to Black Jack game!\nPress any key to start game...");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void ShowHeader(ref GameStatistics stats)
        {
            Console.Clear();
            Console.WriteLine("Round {0}. Enter Your choice (h: Hit, WhiteSpace: Stand):", ++stats.roundNumber);
            Console.WriteLine("(Player - {0}, Dealer - {1}, Draws - {2})", stats.playerWins, stats.dealerWins, stats.draws);
            Console.WriteLine(new string('-', 69));
        }

        public static void ShowHands(Player dealer, Player player)
        {
            Console.WriteLine("Dealer : {0} pts. total", dealer.Points);

            foreach (var card in dealer.Hand)
            {
                Console.Write("{0} ", DisplayCard(card));
            }

            Console.WriteLine();

            Console.WriteLine("Player : {0} pts. total", player.Points);

            foreach (var card in player.Hand)
            {
                Console.Write("{0} ", DisplayCard(card));
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', 69));
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

        public static bool PromptNewRound()
        {
            Console.WriteLine("Press any key to start new game, or \"x\" to exit game...");
            if(Console.ReadKey(true).KeyChar=='x')
            {
                return false;
            }
            return true;
        }

        public static void ShowResults(ref GameStatistics stats, int dealerPoints, int playerPoints)
        {

            if (playerPoints > 21)
            {
                Console.WriteLine("Too much.. Dealer Wins!");
                stats.dealerWins++;
                return;
            }

            if (dealerPoints > 21)
            {
                Console.WriteLine("Player wins! Congrats!");
                stats.playerWins++;
                return;
            }

            if (dealerPoints > playerPoints)
            {
                Console.WriteLine("Dealer wins!");
                stats.dealerWins++;
                return;
            }

            if(dealerPoints < playerPoints)
            {
                Console.WriteLine("Player wins! Congrats!");
                stats.playerWins++;
                return;
            }

            Console.WriteLine("Draw!");
            stats.draws++;
            
        }
    }
}
