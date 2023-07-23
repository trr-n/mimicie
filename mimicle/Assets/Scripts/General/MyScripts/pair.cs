using System;
using System.Collections.Generic;

namespace Self.Utils
{
    public class Pair<TKey, TValue> where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
    {
        public TKey Key;
        public TValue Value;

        public Pair(TKey key = default, TValue value = default)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}