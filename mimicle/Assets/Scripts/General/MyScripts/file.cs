using System.Runtime.CompilerServices;

namespace Feather.Utils
{
    public static class File
    {
        public static string CallerPath([CallerFilePath] string path = "") => path;
        public static int CallerLineNumber([CallerLineNumber] int line = 0) => line;
    }
}
