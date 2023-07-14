using UnityEngine;

namespace Feather.Utils
{
    public enum LogType { Standard, Warning, Error }
    public static class log
    {
        public static void print(this object msg) => Debug.Log($"<color=white>{msg}</color>");
        static void show(this object msg) => Debug.Log($"<color=white>{msg}</color>");
        static void warn(this object msg) => Debug.LogWarning($"<color=yellow>{msg}</color>");
        static void error(this object msg) => Debug.LogError($"<color=red>{msg}</color>");
        public static void show(this object msg, LogType style = LogType.Standard)
        {
            switch (style)
            {
                case LogType.Standard: msg.show(); break;
                case LogType.Warning: msg.warn(); break;
                case LogType.Error: msg.error(); break;
            }
        }
        public static string NewLine(this object msg) => msg + "\r\n";
        public static string Space(this object msg) => msg + " ";
    }
}
