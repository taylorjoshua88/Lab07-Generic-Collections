using System;

namespace GenericCollections
{
    class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            // Demonstrate Deck instantiation
            Deck<Card> deck = new Deck<Card>();
            Console.WriteLine(
                $"Instantiated a new Deck<Card> with {deck.Capacity} capacity and {deck.Length} items.");

            // Demonstrate Deck instantiation with 15 values
            Deck<Card> deck2 = new Deck<Card>
            {
                new Card(CardSuit.Spades, CardRank.Ace),
                new Card(CardSuit.Diamonds, CardRank.Three),
                new Card(CardSuit.Hearts, CardRank.Jack),
                new Card(CardSuit.Spades, CardRank.Nine),
                new Card(CardSuit.Clubs, CardRank.King),
                new Card(CardSuit.Hearts, CardRank.Queen),
                new Card(CardSuit.Diamonds, CardRank.Seven),
                new Card(CardSuit.Spades, CardRank.Eight),
                new Card(CardSuit.Clubs, CardRank.Four),
                new Card(CardSuit.Hearts, CardRank.Five),
                new Card(CardSuit.Diamonds, CardRank.Six),
                new Card(CardSuit.Spades, CardRank.Ten),
                new Card(CardSuit.Diamonds, CardRank.Ace),
                new Card(CardSuit.Clubs, CardRank.King),
                new Card(CardSuit.Hearts, CardRank.Three),
            };

            Console.WriteLine();
            Console.WriteLine($"Instantiated another Deck<Card> with {deck2.Capacity} capacity and {deck2.Length} items.");
            Console.WriteLine();

            foreach (Card card in deck2)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }

            // Demonstrate removing a Card from the Deck collection
            Console.WriteLine();
            Console.WriteLine("Please press a key to remove the Nine of Spades...");
            Console.ReadKey(true);
            Console.WriteLine();

            deck2.Remove(new Card(CardSuit.Spades, CardRank.Nine));

            foreach (Card card in deck2)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }

            // Demonstrate shuffling using Deck.Shuffle()
            Deal();

            Console.WriteLine();
            Console.WriteLine("Please press a key to end this program...");
            Console.ReadKey();
        }

        /// <summary>
        /// Display strings to the console simulating a dealer dealing 15 random
        /// cards and then shuffling them
        /// </summary>
        public static void Deal()
        {
            Console.WriteLine();
            Console.WriteLine("Please press a key to be dealt a hand of cards...");
            Console.ReadKey(true);
            Console.WriteLine();

            Deck<Card> hand = new Deck<Card>();
            Random random = new Random();

            // Create 15 random cards
            for (int i = 0; i < 15; i++)
            {
                hand.Add(new Card((CardSuit)random.Next(4), 
                    (CardRank)random.Next(1, 14)));
            }

            // Display the original hand (Deck) of dealt cards
            foreach (Card card in hand)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }

            Console.WriteLine();
            Console.WriteLine("Please press a key to shuffle your hand...");
            Console.ReadKey(true);
            Console.WriteLine();

            hand.Shuffle();

            // Display the shuffled hand of cards
            foreach (Card card in hand)
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
        }
    }
}
