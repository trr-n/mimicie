using System;

namespace Mimical.Extend
{
    public static class typing
    {
        public static Type type(object obj) => obj.GetType();

        public static float single(this object obj) => (float)obj;
        public static int inte(this object obj) => (int)obj;
        public static string str(this object obj) => (string)obj;
    }
}
