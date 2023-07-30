using UnityEngine;
using static UnityEngine.Application;
using static UnityEngine.Cursor;

namespace Self.Utility
{
    public enum FrameRate { Low = 24, Medium = 30, High = 60, VSync = -1 }
    public enum CursorAppearance { Invisible, Visible }
    public enum CursorRangeOfMotion { InScene = CursorLockMode.Confined, Fixed = CursorLockMode.Locked, Limitless = CursorLockMode.None }

    public static class App
    {
        public static void SetFPS(int fps = -1) => targetFrameRate = fps;

        /// <param name="fps">
        /// Low: 24<br/>
        /// Medium: 30<br/>
        /// High: 60<br/>
        /// VSync: -1<br/>
        /// </param>
        public static void SetFPS(FrameRate fps) => targetFrameRate = ((int)fps);

        public static float GetFPS() => Mathf.Floor(1 / Time.deltaTime);

        public static void SetCursorStatus(CursorAppearance status, CursorRangeOfMotion rangeOfMotion)
        {
            visible = status != CursorAppearance.Invisible;
            lockState = (CursorLockMode)rangeOfMotion;
        }
    }
}