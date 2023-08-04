namespace Self.Utils
{
    public static class Typing
    {
        public static T Cast<T>(this object obj) => (T)obj;

        /// <summary>
        /// 置換したい文字をスペース二つはさんで記述
        /// </summary>
        public static string ReplaceAtOnce(this string target, string before, string after)
        {
            string[] pres = before.Split("  ");

            for (int count = 0; count < pres.Length; count++)
            {
                target = target.Replace(pres[count], after);
            }
            return target;
        }
    }
}
