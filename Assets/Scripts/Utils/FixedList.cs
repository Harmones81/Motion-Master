using System;
using System.Collections;
using System.Collections.Generic;

namespace MotionMaster.Utils
{
    /// <summary>
    /// A custom data structure that represents a list with a fixed (static) size.
    /// </summary>
    /// <typeparam name="T">the object type the list stores</typeparam>
    [System.Serializable]
    public class FixedList<T> : IEnumerable<T>
    {
        private T[] list;
        private int count;

        /// <summary>
        /// the amount of items in the list
        /// </summary>
        public int Count { get { return count; } }
        /// <summary>
        /// checks whether the list has reached its capacity
        /// </summary>
        public bool Full { get { return count >= list.Length; } }

        public FixedList(int size) => list = new T[size];

        /// <summary>
        /// adds an item to the list
        /// </summary>
        /// <param name="item">the item that will be added to the list</param>
        public void Add(T item)
        {
            if (Full)
            {
                string listName = list.ToString();
                Console.WriteLine(String.Format("Capacity reached for list {0}", listName));
                return;
            }

            list[count] = item;
            count++;
        }

        /// <summary>
        /// removes an item from the list
        /// </summary>
        /// <param name="item">the item being removed from the list</param>
        public void Remove(T item)
        {
            if (!Contains(item))
            {
                string itemName = item.ToString();
                string listName = list.ToString();
                Console.WriteLine(String.Format("Item {0} not in list {1}", itemName, listName));
                return;
            }

            int itemIndex = Array.IndexOf(list, item);
            ShiftItems(itemIndex);
            count--;
        }

        /// <summary>
        /// removes an item from the list that is located at an index
        /// </summary>
        /// <param name="index">the index the item is located at</param>
        public void RemoveAt(int index)
        {
            ShiftItems(index);
            count--;
        }

        /// <summary>
        /// checks if the list contains an item
        /// </summary>
        /// <param name="item">the item we are checking for</param>
        /// <returns></returns>
        public bool Contains(T item) => Array.Exists(list, elem => elem.Equals(item));

        /// <summary>
        /// shifts the items in the list accordingly whenever an item gets added or removed
        /// </summary>
        /// <param name="startIndex">the index in the list the items get shifted from</param>
        private void ShiftItems(int startIndex)
        {
            if(startIndex == list.Length - 1)
            {
                list[startIndex] = default;
                return;
            }

            for(int i = startIndex; i < list.Length; i++)
            {
                if(i < list.Length - 1)
                {
                    list[i] = list[(i + 1) % list.Length];
                    continue;
                }

                list[i] = default;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)list).GetEnumerator();
    }
}
