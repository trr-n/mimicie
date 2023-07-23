using System;
using UnityEngine;

namespace Self.Utils
{
    public static class Numeric
    {
        public static float Clamping(this float n, float min, float max) => Mathf.Clamp(n, min, max);
        public static int Clamping(this int n, int min, int max) => Mathf.Clamp(n, min, max);
        public static float Clamp(float n, float min, float max) => Mathf.Clamp(n, min, max);
        public static int Clamp(int n, int min, int max) => Mathf.Clamp(n, min, max);

        public static float Clamp01(float n) => Clamp(n, 0f, 1f);
        public static int Clamp01(int n) => Clamp(n, 0, 1);

        public static float Round(float n, int digit = 0) => MathF.Round(n, digit);
        public static int Round(int n, int digit = 0) => ((int)MathF.Round(n, digit));

        public static int Percent(float n, int digit = 0) => ((int)MathF.Round(n * 100, digit));
        public static int Percent(int whole, int percentage) => Round(whole * percentage / 100);

        public static float Ratio(float w, float t) => (float)w / t;
        public static bool Twins(this float n1, float n2) => Mathf.Approximately(n1, n2);
        public static bool IsPrime(int n)
        {
            if (n < 2 || (n % 2 == 0 && n != 2))
            {
                return false;
            }

            for (int i = 2; i < Mathf.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static int GetEnumLength<T>(this T t) => Enum.GetNames(typeof(T)).Length;
    }
}