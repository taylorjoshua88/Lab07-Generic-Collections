using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenericCollections
{
    /// <summary>
    /// Generic collection holding values of type T. The capacity
    /// of this collection will expand and shrink as its length
    /// surpasses capacity or is reduced below one third its capacity
    /// </summary>
    /// <typeparam name="T">The type of items that can be held
    /// by this collection</typeparam>
    public class Deck<T> : IEnumerable<T>
    {
        // Backing array storage for the Deck collection's items
        private T[] items;

        /// <summary>
        /// The number of items currently being managed by this collection
        /// </summary>
        public int Length { get; private set; } = 0;

        /// <summary>
        /// The capacity of this collection's internal array
        /// for items. Increasing the collection's length past
        /// this value will cause the collection's internal array
        /// capacity to double in size. Decreasing the collection's
        /// Length to a value less than one third of Capacity
        /// will result in the internal array's capacity being
        /// shrunk in size by one half
        /// </summary>
        public int Capacity {
            get {
                return items.Length;
            }
        }

        /// <summary>
        /// Constructor which initializes the backing array
        /// for this collection with the provided capacity of
        /// <paramref name="initialCapacity"/> (default = 10)
        /// </summary>
        /// <param name="initialCapacity">The initial capacity
        /// of the collection. Setting this value to your
        /// predicted capacity requirements can prevent resizing 
        /// and wasted memory overhead as items are added.</param>
        public Deck(int initialCapacity = 10)
        {
            items = new T[initialCapacity];
        }

        /// <summary>
        /// Returns the item held at the specified index
        /// </summary>
        /// <param name="index">The zero-based index of the item to return</param>
        /// <returns>The item of type <typeparamref name="T"/> at
        /// <paramref name="index"/></returns>
        public T GetItem(int index)
        {
            if (index >= Length)
            {
                throw new ArgumentOutOfRangeException("Attempted to " +
                    "access an index beyond the Length of this Deck", nameof(index));
            }

            return items[index];
        }

        /// <summary>
        /// Expands the Capacity (items.Length) of the collection by
        /// multiplying its current capacity by 2
        /// </summary>
        private void Expand()
        {
            T[] expandedItemsArray = new T[Capacity * 2];

            /*
             *  NOTE(taylorjoshua88):
             *    In production code I would probably use Array.Resize or
             *    CopyTo to perform this operation rather than iterating
             *    over the entire array copying into a temporary array.
            */
            for (int itemIdx = 0; itemIdx < Capacity; itemIdx++)
            {
                expandedItemsArray[itemIdx] = items[itemIdx];
            }

            items = expandedItemsArray;
        }

        /// <summary>
        /// Shrinks the collection by dividing its capacity by 2
        /// 
        /// </summary>
        /// <exception cref="ApplicationException">An attempt was made to destructively
        /// shrink Capacity below Length, which would result in a loss of data</exception>"
        private void Shrink()
        {
            int newCapacity = Capacity / 2;

            // Prevent shrinking of the array that would result in items being lost
            // from the collection
            if (newCapacity < Length)
            {
                throw new ApplicationException("Attempt to destructively shrink Capacity " +
                    "below the Length of items within the Deck collection");
            }

            T[] shrunkItemsArray = new T[newCapacity];

            /*
             *  NOTE(taylorjoshua88):
             *    In production code I would probably use Array.Resize or
             *    CopyTo to perform this operation rather than iterating
             *    over the entire array copying into a temporary array.
            */
            for (int itemIdx = 0; itemIdx < Length; itemIdx++)
            {
                shrunkItemsArray[itemIdx] = items[itemIdx];
            }

            items = shrunkItemsArray;
        }

        /// <summary>
        /// Finds the index of the first instance of <paramref name="searchItem"/>
        /// within this collection
        /// </summary>
        /// <param name="searchItem">An item of type <typeparamref name="T"/>
        /// to find the index for</param>
        /// <returns>The index of the first instance of <paramref name="searchItem"/>
        /// within the collection</returns>
        public int Find(T searchItem)
        {
            for (int itemIdx = 0; itemIdx < Length; itemIdx++)
            {
                if (searchItem.Equals(items[itemIdx]))
                {
                    return itemIdx;
                }
            }

            return -1;
        }

        /// <summary>
        /// Adds a new item, <paramref name="newItem"/>, to the end
        /// of the collection.
        /// </summary>
        /// <param name="newItem">An item of type <typeparamref name="T"/>
        /// to be added to the end of the collection</param>
        public void Add(T newItem)
        {
            if (Capacity <= Length)
            {
                Expand();
            }

            items[Length++] = newItem;
        }

        /// <summary>
        /// Finds the item <paramref name="itemToRemove"/> and removes
        /// it from the collection by shifting all items after it up
        /// by one index and reducing Length by one. If the new Length
        /// is less than one third of its capacity, then the capacity
        /// will be shrunk in half
        /// </summary>
        /// <param name="itemToRemove">The item to be removed from the
        /// collection</param>
        public void Remove(T itemToRemove)
        {
            int removedItemIdx = Find(itemToRemove);

            // Item was not found; just return without doing anything
            if (removedItemIdx < 0)
            {
                return;
            }

            /* 
             *  NOTE(taylorjoshua88):
             *    If the ordering of our collection were not important,
             *    the following would be far more efficient than having
             *    to shift all items up in the collection {O(1) vs. O(n)}
             *    Decks should not have their ordering affected unless
             *    they are shuffled though, so we cannot use this:
             *
             *    items[removedItemIdx] = items[--Length];
            */
            
            // Iterate through all indexes after our removed item's index and shift
            // those items back one index, resulting in a smaller collection
            for (int curItemIdx = removedItemIdx + 1; curItemIdx < Length; curItemIdx++)
            {
                items[curItemIdx - 1] = items[curItemIdx];
            }

            // If our new Length is less than a third of Capacity, then trigger
            // a shrinking of the items array (by one half)
            if (--Length < Capacity / 3)
            {
                Shrink();
            }
        }

        /// <summary>
        /// Swaps the item at index <paramref name="indexA"/> with the 
        /// item at index <paramref name="indexB"/> 
        /// </summary>
        /// <param name="indexA">Index of the first item to be swapped</param>
        /// <param name="indexB">Index of the second item to be swapped</param>
        public void SwapItems(int indexA, int indexB)
        {
            // If the indexes match, then swapping would do nothing
            if (indexA == indexB)
            {
                return;
            }

            T temporaryItem = items[indexA];

            items[indexA] = items[indexB];
            items[indexB] = temporaryItem;
        }

        /// <summary>
        /// Iterates through the entire collection swapping items with
        /// other items at random indexes
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random();

            // Go through every item in the collection and swap it
            // with another item at a random index
            for (int curIndex = 0; curIndex < Length; curIndex++)
            {
                SwapItems(curIndex, random.Next(Length));
            }
        }

        /// <summary>
        /// Returns an IEnumerator for type <typeparamref name="T"/>
        /// </summary>
        /// <returns>An IEnumerator for type <typeparamref name="T"/>
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int itemIdx = 0; itemIdx < Length; itemIdx++)
            {
                yield return items[itemIdx];
            }
        }

        /// <summary>
        /// Returns a non-generic IEnumerator for collection initializers
        /// </summary>
        /// <returns>An IEnumerator object for this class</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
    }
}
