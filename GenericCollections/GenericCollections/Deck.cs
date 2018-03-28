using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCollections
{
    public class Deck<T>
    {
        private T[] items;

        public int Length { get; private set; } = 0;

        /// <summary>
        /// The capacity of this collection's internal array
        /// for items. Increasing the collection's length past
        /// this value will cause the collection's internal array
        /// to expand by a factor of ResizeFactor. Decreasing the
        /// collection's Length to a value less than 
        /// Capacity / (2 * ResizeFactor) will result in the Capacity
        /// being reduced to Capacity / ResizeFactor
        /// </summary>
        public int Capacity {
            get {
                return items.Length;
            }
        }

        /// <summary>
        /// The factor by which this collection's capacity expands
        /// and shrinks in response to items being removed or added
        /// to the collection. ResizeFactor must be greater than 1.
        /// </summary>
        private int _resizeFactor = 2;
        public int ResizeFactor {
            get {
                return _resizeFactor;
            }
            set {
                if (_resizeFactor < 1)
                {
                    throw new ArgumentOutOfRangeException(
                        "ResizeFactor must be greater than 1.", nameof(value));
                }

                _resizeFactor = value;
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
        /// Expands the Capacity (items.Length) of the collection by
        /// multiplying its current Capacity by ResizeFactor
        /// </summary>
        private void Expand(int factor = 2)
        {
            T[] expandedItemsArray = new T[Capacity * factor];

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
        /// Shrinks the collection by dividing its Capacity (items.Length)
        /// by ResizeFactor
        /// </summary>
        /// <exception cref="ArgumentException">An attempt was made to destructively
        /// shrink Capacity below Length, which would result in a loss of data</exception>"
        private void Shrink(int divisor = 2)
        {
            int newCapacity = Capacity / divisor;

            // Prevent shrinking of the array that would result in items being lost
            // from the collection
            if (newCapacity < Length)
            {
                throw new ArgumentException("Attempt to destructively shrink Capacity " +
                    "below the Length of items within the Deck collection", nameof(divisor));
            }

            T[] shrunkItemsArray = new T[newCapacity];

            /*
             *  NOTE(taylorjoshua88):
             *    In production code I would probably use Array.Resize or
             *    CopyTo to perform this operation rather than iterating
             *    over the entire array copying into a temporary array.
            */
            for (int itemIdx = 0; itemIdx < Capacity; itemIdx++)
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
        /// is less than Capacity / (2 * ResizeFactor), then Capacity
        /// will be modified to Capacity / ResizeFactor
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
             *    to shift all items up in the collection. Decks should
             *    not have their ordering affected unless they are
             *    shuffled though, so we cannot use this:
             *
             *    items[removedItemIdx] = items[--Length];
            */
            
            // Iterate through all indexes after our removed item's index and shift
            // those items back one index, resulting in a smaller collection
            for (int curItemIdx = removedItemIdx + 1; curItemIdx < Length; curItemIdx++)
            {
                items[curItemIdx - 1] = items[curItemIdx];
            }

            // If our new Length is less than Capacity / (2 * ResizeFactor) then
            // shrink our Capacity to Capacity / ResizeFactor. Making this condition
            // triggered by Capacity / (2 * ResizeFactor) will help prevent Capacity
            // being thrashed around if adding and removing at a critical value
            if (--Length < Capacity / (2 * ResizeFactor))
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
    }
}
