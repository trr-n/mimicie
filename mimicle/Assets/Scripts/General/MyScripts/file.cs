using System.Runtime.CompilerServices;

namespace Mimical.Extend
{
    public static class file
    {
        public static string CallerPath([CallerFilePath] string path = "")
        => path;

        public static int CallerLineNumber([CallerLineNumber] int line = 0)
        => line;

        public static string Caller(
            [CallerFilePath] string path = "",
            [CallerLineNumber] int line = 0
        ) => $"{path}: {line}";
    }
}
