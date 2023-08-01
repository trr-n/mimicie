using System;
using System.Collections;
using System.Collections.Generic;

namespace Self.Utility
{
    public class Bag<T>
    {
        T[] collection;
        int length;
        int capacity;

        public T this[int index] => collection[index];

        public int Length => length;

        public Bag()
        {
            this.capacity = 10;
            collection = new T[capacity];
            length = 0;
        }

        public Bag(int capacity)
        {
            this.capacity = capacity;
            collection = new T[capacity];
            length = 0;
        }

        public void Add(T item)
        {
            if (length == capacity)
            {
                capacity += 8;
                T[] items = new T[capacity];

                for (int i = 0; i < length; i++)
                {
                    items[i] = collection[i];
                }

                collection = items;
            }


            collection[length] = item;
            length++;
        }

        public void Remove()
        {

        }
    }
}