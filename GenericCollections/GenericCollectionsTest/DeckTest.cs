using System;
using Xunit;
using GenericCollections;

namespace GenericCollectionsTest
{
    public class DeckTest
    {
        [Fact]
        public void CanAddValueType()
        {
            // Arrange
            Deck<int> deck = new Deck<int>();

            // Act
            deck.Add(5);

            // Assert
            Assert.Equal(1, deck.Length);
        }

        [Fact]
        public void CanAddReferenceType()
        {
            // Arrange
            Deck<Card> deck = new Deck<Card>();

            // Act
            deck.Add(new Card(CardSuit.Spades, CardRank.Ace));

            // Assert
            Assert.Equal(1, deck.Length);
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 0)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 10)]
        public void GetItemTest(int[] seedValues, int indexToGet)
        {
            // Arrange
            Deck<int> deck = new Deck<int>();

            foreach (int seedValue in seedValues)
            {
                deck.Add(seedValue);
            }

            // Act
            int itemFromDeck = deck.GetItem(indexToGet);

            // Act + Assert
            Assert.Equal(seedValues[indexToGet], itemFromDeck);
        }

        [Fact]
        public void CanSwapItems()
        {
            // Arrange
            Deck<int> deck = new Deck<int> { 1, 2, 3, 4 };

            // Act
            deck.SwapItems(0, 2);

            // Assert
            Assert.Equal(1, deck.GetItem(2));
        }

        [Fact]
        public void CanTestDeckEquality()
        {
            // Arrange
            Deck<int> originalDeck = new Deck<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Deck<int> shuffledDeck = new Deck<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Assert
            Assert.Equal(originalDeck, shuffledDeck);
        }

        [Fact]
        public void CanShuffleItems()
        {
            // Arrange
            Deck<int> originalDeck = new Deck<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Deck<int> shuffledDeck = new Deck<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            shuffledDeck.Shuffle();

            // Assert
            Assert.NotEqual(originalDeck, shuffledDeck);
        }

        [Theory]
        [ClassData(typeof(FindCardTestData))]
        public void FindCardTest(Card[] seedCards, Card searchCard, int expectedIndex)
        {
            // Arrange
            Deck<Card> deck = new Deck<Card>();

            foreach (Card seedCard in seedCards)
            {
                deck.Add(seedCard);
            }

            // Act
            int foundIndex = deck.Find(searchCard);

            // Assert
            Assert.Equal(expectedIndex, foundIndex);
        }

        [Fact]
        public void CanRemoveItem()
        {
            // Arrange
            Deck<Card> deck = new Deck<Card>
            {
                new Card(CardSuit.Spades, CardRank.Ace),
                new Card(CardSuit.Clubs, CardRank.Three),
                new Card(CardSuit.Diamonds, CardRank.Ten)
            };

            // Act
            deck.Remove(new Card(CardSuit.Clubs, CardRank.Three));

            // Assert
            Assert.Equal(2, deck.Length);
        }

        [Fact]
        public void CanUseInitializerPastTenItems()
        {
            // Arrange
            Deck<int> deck = new Deck<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            // Assert
            Assert.True(deck.Length > 10);
        }

        [Fact]
        public void CanExpandCapacityWithAdd()
        {
            // Arrange
            const int initialCapacity = 5;
            Deck<int> deck = new Deck<int>(initialCapacity);


            // Act
            for (int i = 0; i < initialCapacity + 1; i++)
            {
                deck.Add(i);
            }

            // Assert
            Assert.True(deck.Capacity == initialCapacity * 2);
        }

        [Fact]
        public void CanShrinkCapacityWithRemove()
        {
            // Arrange
            Deck<int> deck = new Deck<int>(6) { 1, 2, 3, 4, 5, 6 };

            // Act
            // Bring deck.Length to a value < deck.Capacity / 3
            deck.Remove(1);
            deck.Remove(2);
            deck.Remove(3);
            deck.Remove(4);
            deck.Remove(5);

            // Assert
            // Initial capacity was 6 and we triggered a shrink: 6 / 2 = 3
            Assert.Equal(3, deck.Capacity);
        }

        [Fact]
        public void CanUseForeach()
        {
            // Arrange
            Deck<int> deck = new Deck<int> { 1, 2, 3, 4 };
            int total = 0;
            const int expectedTotal = 1 + 2 + 3 + 4;

            // Act
            foreach (int item in deck)
            {
                total += item;
            }

            // Assert
            Assert.Equal(expectedTotal, total);
        }
    }
}
