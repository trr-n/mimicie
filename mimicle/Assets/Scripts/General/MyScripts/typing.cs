namespace UnionEngine.Extend
{
    public static class Typing
    {
        public static T Cast<T>(this object obj) => (T)(object)obj;
        public static float Single(this object obj) => (float)obj;
        public static int Int(this object obj) => (int)obj;
        public static double Double(this object obj) => (double)obj;
    }
}
