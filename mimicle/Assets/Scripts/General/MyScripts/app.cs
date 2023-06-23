using static UnityEngine.Application;

namespace Mimical.Extend
{
    public enum FrameRate { Low = 24, Medium = 30, High = 60, VSync = -1 }
    public static class App
    {
        public static void SetFps(int fps = -1) => targetFrameRate = fps;
        public static void SetFps(FrameRate fps) => targetFrameRate = ((int)fps);
    }
}