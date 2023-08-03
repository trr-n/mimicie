using System;
using System.Collections.Generic;

namespace Self.Utils
{
    public class Pair<TKey, TValue> where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
    {
        public TKey Key;
        public TValue Value;

        public Pair() { }

        public Pair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public class Pairs<TKey, TValue>
    {
        TKey[] keys;
        TValue[] values;

        int capacity;

        public Pairs() { }

        public Pairs(int capacity)
        {
            this.capacity = capacity;
            keys = new TKey[capacity];
            values = new TValue[capacity];
        }
    }
}