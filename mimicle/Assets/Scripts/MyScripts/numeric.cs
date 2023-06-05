using UnityEngine;

namespace Mimical.Extend
{
    public static class numeric
    {
        public static float Clamping(this float target, float min, float max)
        => Mathf.Clamp(target, min, max);

        public static int Clamping(this int target, int min, int max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static float Clamp(float target, float min, float max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static int Clamp(int target, int min, int max)
        => UnityEngine.Mathf.Clamp(target, min, max);

        public static float Round(float n, int digit) => System.MathF.Round(n, digit);

        // public static void sample(out int n) => n = 5;
    }
}