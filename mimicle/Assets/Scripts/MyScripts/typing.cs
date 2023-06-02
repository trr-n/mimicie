using System;

namespace Mimical.Extend
{
    public static class typing
    {
        public static Type type(object obj) => obj.GetType();

        public static float ToSingle(this object obj) => (float)obj;
        public static int ToInt(this object obj) => (int)obj;
    }
}
