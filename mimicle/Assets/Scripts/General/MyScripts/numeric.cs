using System;
using System.Linq;
using System.Collections.Generic;
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

        public static int Round(int n, int digit)
        => ((int)MathF.Round(n, digit));

        public static int Percent(float n, int digit = 0)
        => ((int)MathF.Round(n * 100, digit));

        public static bool AlmostSame(this float n1, float n2)
        => Mathf.Approximately(n1, n2);

        public static bool AlmostSame(Vector3 n1, Vector3 n2)
        {
            return Mathf.Approximately(n1.x, n2.x) &&
                Mathf.Approximately(n1.y, n2.y) &&
                Mathf.Approximately(n1.z, n2.z);
        }

        // TODO
        public static bool IsPrime(int n)
        {
            if (n < 2 || (n % 2 == 0 && n != 2))
                return false;
            var ns = new List<bool>();
            for (int i = 2; i < n; i++)
                ns.Add(i % n == 0);
            return ns.All(b => true);
        }
    }
}