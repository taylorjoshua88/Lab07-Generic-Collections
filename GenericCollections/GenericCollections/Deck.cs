using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCollections
{
    public class Deck<T>
    {
        private T[] items;

        public int Length { get; private set; } = 0;

        public int Capacity {
            get {
                return items.Length;
            }
        }

        public Deck(int initialCapacity = 10)
        {
            items = new T[initialCapacity];
        }

        private void Expand(int factor = 2)
        {
            T[] expandedItemsArray = new T[Capacity * factor];

            /*
             *  NOTE(taylorjoshua88):
             *    In production code I would probably use Array.Resize or
             *    CopyTo to perform this operation rather than iterating
             *    over the entire array.
            */
            for (int itemIdx = 0; itemIdx < Capacity; itemIdx++)
            {
                expandedItemsArray[itemIdx] = items[itemIdx];
            }

            items = expandedItemsArray;
        }

        private void Shrink(int divisor = 2)
        {
            int newCapacity = Capacity / divisor;

            // Prevent shrinking of the array that would result in items being lost
            // from the collection
            if (newCapacity < Length)
            {
                throw new ArgumentException("Attempt to destructively shrink array Capacity " +
                    "below the Length of items within the Deck collection.", nameof(divisor));
            }

            T[] shrunkItemsArray = new T[newCapacity];

            /*
             *  NOTE(taylorjoshua88):
             *    In production code I would probably use Array.Resize or
             *    CopyTo to perform this operation rather than iterating
             *    over the entire array.
            */
            for (int itemIdx = 0; itemIdx < Capacity; itemIdx++)
            {
                shrunkItemsArray[itemIdx] = items[itemIdx];
            }

            items = shrunkItemsArray;
        }

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

        public void Add(T newItem)
        {
            if (Capacity <= Length)
            {
                Expand();
            }

            items[Length++] = newItem;
        }

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

            // If our new length is less than half of the capacity of the internal
            // array storage, then shrink the array's capacity by half to save memory
            if (--Length < Capacity / 2)
            {
                Shrink();
            }
        }
    }
}
