using System.Runtime.CompilerServices;

namespace Mimical.Extend
{
    public static class file
    {
        public static string caller_path(
            [CallerFilePath] string path = "")
        => path;

        public static int caller_line_number(
            [CallerLineNumber] int line = 0)
        => line;

        public static string caller(
            [CallerFilePath] string path = "",
            [CallerLineNumber] int line = 0
        ) => $"{path}: {line}";
    }
}
