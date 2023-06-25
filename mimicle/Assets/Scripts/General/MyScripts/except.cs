using System;

namespace Mimical.Extend
{
    [Serializable]
    public class Karappoyanke : Exception
    {
        public Karappoyanke() : base("1つはいれろやエクセプション") {; }
        public Karappoyanke(string msg) : base(msg) {; }
    }

    [Serializable]
    public class KanshakumotiException : Exception
    {
        public KanshakumotiException() : base("癇癪餅") {; }
        public KanshakumotiException(string msg) : base(msg) {; }
    }
}