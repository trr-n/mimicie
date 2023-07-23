namespace Self.Utils
{
    public static class Typing
    {
        public static T Cast<T>(this object obj) => (T)(object)obj;
    }
}
