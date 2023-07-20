using static UnityEngine.Application;

namespace MyGame.Utils
{
    public enum FrameRate { Low = 24, Medium = 30, High = 60, VSync = -1 }
    public static class App
    {
        public static void Fps(int fps = -1) => targetFrameRate = fps;
        public static void Fps(FrameRate fps) => targetFrameRate = ((int)fps);
    }
}