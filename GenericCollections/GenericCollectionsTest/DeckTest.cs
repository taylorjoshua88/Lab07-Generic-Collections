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
    }
}
