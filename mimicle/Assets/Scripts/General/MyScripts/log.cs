using UnityEngine;

namespace Mimical.Extend
{
    public enum LogStyle { Standard, Warning, Error }

    public static class log
    {
        public static void print(this object msg) => Debug.Log($"<color=white>{msg}</color>");
        static void show(this object msg) => Debug.Log($"<color=white>{msg}</color>");
        static void warn(this object msg) => Debug.LogWarning($"<color=yellow>{msg}</color>");
        static void error(this object msg) => Debug.LogError($"<color=red>{msg}</color>");
        public static void show(this object msg, LogStyle style = LogStyle.Standard)
        {
            switch (style)
            {
                case LogStyle.Standard: msg.show(); break;
                case LogStyle.Warning: msg.warn(); break;
                case LogStyle.Error: msg.error(); break;
            }
        }
        public static string newline(this object msg) => msg + "\r\n";
        public static string space(this object msg) => msg + " ";
    }
}
