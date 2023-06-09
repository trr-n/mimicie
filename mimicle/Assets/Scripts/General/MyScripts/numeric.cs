using System;
using UnityEngine;

namespace Mimical.Extend
{
    public static class numeric
    {
        public static float Clamping(this float n, float min, float max)
        => Mathf.Clamp(n, min, max);

        public static int Clamping(this int n, int min, int max)
        => Mathf.Clamp(n, min, max);


        public static float Clamp(float n, float min, float max)
        => Mathf.Clamp(n, min, max);

        public static int Clamp(int n, int min, int max)
        => Mathf.Clamp(n, min, max);


        public static float Round(float n, int digit)
        => MathF.Round(n, digit);

        public static float Percent(float n, int digit = 0)
        => MathF.Round(n * 100, digit);

        // public static void sample(out int n) => n = 5;


        // public static float Max(this float n, float a)
        // => Math.Max(n, a);

        // public static float Min(this float n, float a)
        // => Math.Min(n, a);
    }
}