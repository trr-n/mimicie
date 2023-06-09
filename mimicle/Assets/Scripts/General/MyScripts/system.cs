using UnityEngine;
using static UnityEngine.SystemInfo;

namespace Mimical.Extend
{
    public static class system
    {
        public static string OS()
        {
            var os = operatingSystem;
            // var name = os.Split(" ");
            // return name[0].space() + name[1];
            return os;
        }

        public static int RAM() => systemMemorySize / 1000;

        public static string CPU()
        {
            var cpu = processorType;
            var core = processorCount;
            // AMD Ryzen 5 3600 6-Core Processor
            // var name = gpu.Split(" ");
            // return name[1].space() + name[2].space() + name[3].space() +
            //     core.space() + "threads";
            return cpu;
        }

        public static string GPU()
        {
            var gpu = graphicsDeviceName;
            var vram = graphicsMemorySize / 1000;
            // NVIDIA GeForce RTX 3060
            // var name = gpu.Split(" ");
            // return name[2].space() + name[3].space() + vram + "GB";
            return gpu.space() + vram + "GB";
        }
    }
}