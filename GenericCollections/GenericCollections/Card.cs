using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCollections
{
    public class Card : IEquatable<Card>
    {
        public CardSuit Suit { get; private set; }
        public CardRank Rank { get; private set; }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        #region IEquatable<Card> implementation
        public bool Equals(Card other)
        {
            return other != null &&
                   Suit == other.Suit &&
                   Rank == other.Rank;
        }

        public override bool Equals(object other)
        {
            if (other is Card)
            {
                return Equals((Card)other);
            }

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            int hashCode = -90415385;
            hashCode = hashCode * -1521134295 + Suit.GetHashCode();
            hashCode = hashCode * -1521134295 + Rank.GetHashCode();
            return hashCode;
        }
        #endregion
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
