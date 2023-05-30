using UnityEngine;

namespace Mimic.Extend
{
    public static class numeric
    {
        public static float clamping(this float target, float min, float max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static int clamping(this int target, int min, int max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static float clamp(float target, float min, float max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static int clamp(int target, int min, int max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        // public static void clamp1(out float clamped, float min, float max)
        // {
        //     clamped = 0;
        //     clamped = UnityEngine.Mathf.Clamp(clamped, max, max);
        // }

        public static float round(float n, int digit) => System.MathF.Round(n, digit);

        // public static void sample(out int n) => n = 5;
    }
}