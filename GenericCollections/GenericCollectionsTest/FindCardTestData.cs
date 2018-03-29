using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GenericCollections;

namespace GenericCollectionsTest
{
    public class FindCardTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new Card[]
                {
                    new Card(CardSuit.Hearts, CardRank.Queen),
                    new Card(CardSuit.Spades, CardRank.Ace),
                    new Card(CardSuit.Diamonds, CardRank.Four),
                    new Card(CardSuit.Clubs, CardRank.Seven)
                },
                new Card(CardSuit.Spades, CardRank.Ace),
                1
            };

            yield return new object[] {
                new Card[]
                {
                    new Card(CardSuit.Hearts, CardRank.Queen),
                    new Card(CardSuit.Spades, CardRank.Ace),
                    new Card(CardSuit.Diamonds, CardRank.Four),
                    new Card(CardSuit.Clubs, CardRank.Seven)
                },
                new Card(CardSuit.Hearts, CardRank.Queen),
                0
            };

            yield return new object[] {
                new Card[]
                {
                    new Card(CardSuit.Hearts, CardRank.Queen),
                    new Card(CardSuit.Spades, CardRank.Ace),
                    new Card(CardSuit.Diamonds, CardRank.Four),
                    new Card(CardSuit.Clubs, CardRank.Seven)
                },
                new Card(CardSuit.Clubs, CardRank.Seven),
                3
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
