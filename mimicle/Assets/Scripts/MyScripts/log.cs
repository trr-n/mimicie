namespace Mimical.Extend
{
    public static class log
    {
        // public static void show(this object msg)
        // => UnityEngine.Debug.Log(
        //        $"<color=white>{msg}</color> <size=10>{file.caller_path()}: {file.caller_line_number()}</size>");

        // public static void warn(this object msg)
        // => UnityEngine.Debug.LogWarning(
        //         $"<color=orange>{msg}</color> <size=10>{file.caller_path()}: {file.caller_line_number()}</size>");

        // public static void error(this object msg)
        // => UnityEngine.Debug.LogError(
        //        $"<color=red>{msg}</color> <size=10>{file.caller_path()}: {file.caller_line_number()}</size>");

        public static void print(object msg) => UnityEngine.Debug.Log($"<color=white>{msg}</color>");

        public static void show(this object msg) => UnityEngine.Debug.Log($"<color=white>{msg}</color>");
        public static void warn(this object msg) => UnityEngine.Debug.LogWarning($"<color=yellow>{msg}</color>");
        public static void error(this object msg) => UnityEngine.Debug.LogError($"<color=red>{msg}</color>");

        public static string newline(this object msg) => msg + "\r\n";
    }
}
