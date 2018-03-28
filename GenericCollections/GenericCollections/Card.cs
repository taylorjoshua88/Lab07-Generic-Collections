using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCollections
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardRank Rank { get; private set; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

    public enum CardSuit : byte
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }

    public enum CardRank : byte
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
