using UnityEngine;
using static UnityEngine.SystemInfo;

namespace Mimical.Extend
{
    public static class system
    {
        public static string OS() => operatingSystem;
        public static int RAM() => systemMemorySize / 1000;
        public static string CPU() => processorType;
        public static string GPU() => graphicsDeviceName.space() + graphicsMemorySize / 1000 /*1024*/ + "GB";
        public static string Info() { return null; }
    }
}