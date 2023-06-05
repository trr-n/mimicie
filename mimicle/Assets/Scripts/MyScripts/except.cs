using System;

namespace Mimical.Extend
{
    [Serializable]
    public class NanmoHaittehenwaException : Exception
    {
        public NanmoHaittehenwaException() : base("1つはいれろやエクセプション") { }

        public NanmoHaittehenwaException(string msg) : base(msg) { }
    }

    [Serializable]
    public class KanshakumotiException : Exception
    {
        public KanshakumotiException() : base("癇癪餅") { }

        public KanshakumotiException(string msg) : base(msg) { }
    }
}