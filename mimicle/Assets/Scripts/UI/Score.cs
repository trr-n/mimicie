using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Score : MonoBehaviour
    {
        static int scoreReductionRatio = 10;
        static int now = 0;
        public int Now => now;
        public static int Time() => ((int)time.r(0));
        public static void Add(int amount) => now += amount;
        public static int Final() => Numeric.Round(now - Time() * scoreReductionRatio, 0);
    }
}
