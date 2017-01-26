using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        int round = 0;
        CardDeck cardDeck; //52 cards

        Player dealer;
        Player player;

        int playerWins = 0;
        int dealerWins = 0;
        int draws = 0;

        public Game(int numOfPlayers = 1)
        {
            cardDeck = new CardDeck();
            dealer = new Player();
            player = new Player();
            while (Start())
            {
                dealer.Clear();
                player.Clear();
                if (cardDeck.needShuffle)
                    cardDeck.Shuffle();
            }
        }

        //Game cycle
        bool Start()
        {
            bool exitGame = false; //exit flag
            bool newGame = true; //restart game flag

            Console.Clear();
            Console.WriteLine("Round {0}. Enter Your choice (x: Exit, m: More, WhiteSpace: Enough): \n(Player - {1}, Dealer - {2}, Draws - {3})", ++round, playerWins, dealerWins, draws);
            
            FirstSet();

            exitGame = PlayersMove();
            if (exitGame)
            {
                newGame = false;
                return newGame;
            }

            DealersMove();

            return ShowResults();

        }

        //first set of cards. Dealer - 1 card, Player - 2 cards
        void FirstSet()
        {
            dealer.TakeOne(cardDeck.GiveOne());
            player.TakeOne(cardDeck.GiveOne());
            player.TakeOne(cardDeck.GiveOne());

            ShowHands();
        }

        //display players hands
        void ShowHands()
        {
            Console.WriteLine(new string('-', 69));

            Console.WriteLine("Dealer : {0} pts. total", dealer.Points);
            foreach (var card in dealer.Hand)
                Console.Write("{0} ", card);

            Console.WriteLine();

            Console.WriteLine("Player : {0} pts. total", player.Points);
            foreach (var card in player.Hand)
                Console.Write("{0} ", card);

            Console.WriteLine();
            Console.WriteLine(new string('-', 69));
        }

        //Player's move. Returns true to exit game, false to continue game
        bool PlayersMove() 
        {
            char answer;

            while (true)
            {
                if (player.Points != 21)
                    answer = Console.ReadKey(true).KeyChar;
                else answer = ' ';
                switch (answer)
                {
                    case 'x': return true; //exit game
                    case 'm':                                   //Take one more card
                        player.TakeOne(cardDeck.GiveOne());
                        ShowHands();
                        if (player.Points >= 21)
                            return false; //end turn
                        break;
                    case ' ':
                        return false;   //end turn
                    default: break;
                }
            }
        }

        //Dealer's move
        void DealersMove()
        {
            if (player.Points <= 21) //If player's got too much, no need in dealers move
            {
                while (dealer.Points < 17)
                    dealer.TakeOne(cardDeck.GiveOne());
                ShowHands();
            }
        }

        //Show results of this round
        bool ShowResults() //returns true to start new game
        {
            int dealerPoints = dealer.Points;
            int playerPoints = player.Points;

            if (playerPoints > 21)
            {
                Console.WriteLine("Too much.. Dealer Wins!");
                dealerWins++;
            }
            else if (dealerPoints > 21 || playerPoints > dealerPoints)
            {
                Console.WriteLine("Player wins! Congrats!");
                playerWins++;
            }
            else if (dealerPoints > playerPoints)
            {
                Console.WriteLine("Dealer wins!");
                dealerWins++;
            }
            else
            {
                Console.WriteLine("Draw!");
                draws++;
            }
            Console.WriteLine("Press any key to start new game, or \"x\" to exit game...");
            if (Console.ReadKey(true).KeyChar == 'x')
                return false;

            return true;

        }
    }

    class Player
    {
        //Players cards
        List<Card> hand = new List<Card>();

        //Cards accessor
        public List<Card> Hand
        {
            get
            {
                return hand;
            }
        }

        //total points
        public int Points
        {
            get
            {
                int result = 0, value = 0;
                bool gotAce = false;
                foreach (var card in hand)
                {
                    if (int.TryParse(card.Value, out value)) //2-10
                        result += value;
                    else if (card.Value == "J" || card.Value == "Q" || card.Value == "K") //J,Q,K
                        result += 10;
                    else
                    {
                        result++;   //Ace (1)
                        gotAce = true;
                    }
                }
                if (result < 12 && gotAce)
                    result += 10;   //Ace (11)
                return result;
            }
        }

        //take one card from card deck
        public void TakeOne(Card card)
        {
            hand.Add(card);
        }

        //Clear player's hand
        public void Clear()
        {
            hand.Clear();
        }

    }

    //Deck of cards. Fill, Shuffle, DealOne
    class CardDeck
    {
        //card suites
        readonly char[] suites = new char[] { '\u2663', '\u2660', '\u2666', '\u2665' };

        //blank card deck
        readonly List<Card> cards = new List<Card>();

        //shuffled deck, ready to deal
        Stack<Card> shuffled = new Stack<Card>();

        static Random rand = new Random();

        // Minimum cards quantity in cards deck before shuffle
        int Deadline { get; }

        //need shuffle flag/restart card deck
        public bool needShuffle = false;

        public CardDeck(int deadline = 26)
        {
            //Creating blank ordered deck
            foreach (var suite in suites)
            {
                for (int i = 2; i <= 10; i++)
                {
                    cards.Add(new Card(suite, i.ToString())); // string.Concat(" ", i)));
                }
                cards.Add(new Card(suite, "J"));
                cards.Add(new Card(suite, "Q"));
                cards.Add(new Card(suite, "K"));
                cards.Add(new Card(suite, "A"));
            }

            //Shuffle it
            Shuffle();

            //Set deadline before next shuffle
            Deadline = deadline;
        }

        //Generating shuffled stack of cards
        public void Shuffle()
        {
            if (shuffled.Count > 0)
                shuffled.Clear();

            var temp = cards.OrderBy(x => rand.Next());
            foreach (var item in temp)
                shuffled.Push(item);

        }

        public Card GiveOne()
        {
            if (shuffled.Count < Deadline)
                needShuffle = true;

            return shuffled.Pop();
        }
    }

    //Playing card template
    class Card
    {
        public char Suite { get; }
        public string Value { get; }

        public Card(char suite, string value)
        {
            Suite = suite;
            Value = value;
        }

        public override string ToString()
        {
            return string.Concat("[", Suite, " ", Value, "]");
        }
    }

}
