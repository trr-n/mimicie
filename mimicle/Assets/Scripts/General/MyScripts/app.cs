using UnityEngine;
using static UnityEngine.Application;

namespace Mimical.Extend
{
    public enum Rate { Low = 24, Medium = 30, High = 60, VSync = -1 }

    public static class app
    {
        public static void SetFps(int fps = -1) => targetFrameRate = fps;
        public static void SetFps(Rate fps) => targetFrameRate = ((int)fps);
    }
}