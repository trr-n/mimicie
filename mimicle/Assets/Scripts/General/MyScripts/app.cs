using TMPro;
using UnityEngine;
using static UnityEngine.Application;
using static UnityEngine.QualitySettings;
using static UnityEngine.Cursor;

namespace Self.Utils
{
    public enum FrameRate { Low = 30, Medium = 60, High = 144, Ultra = 200, VSync = -1 }
    public enum CursorAppearance { Invisible, Visible }
    public enum CursorRangeOfMotion { InScene = CursorLockMode.Confined, Fixed = CursorLockMode.Locked, Limitless = CursorLockMode.None }

    public class App
    {
        public static void SetFPS(int fps = -1) => targetFrameRate = fps;
        public static void SetFPS(FrameRate fps)
        {
            switch (fps)
            {
                case FrameRate.VSync:
                    vSyncCount = 1;
                    break;
                default:
                    vSyncCount = 0;
                    targetFrameRate = (int)fps;
                    break;
            }
        }
        public static float GetFPS => Mathf.Floor(1 / Time.deltaTime);

        public static void SetGravity(Vector3 gravity) => Physics2D.gravity = gravity;

        public static void SetCursorStatus(CursorAppearance appear, CursorRangeOfMotion rangeOfMotion)
        {
            visible = appear == CursorAppearance.Visible;
            lockState = (CursorLockMode)rangeOfMotion;
        }

        public static bool CurrentTimeScale(float scale) => Time.timeScale == scale;
    }
}