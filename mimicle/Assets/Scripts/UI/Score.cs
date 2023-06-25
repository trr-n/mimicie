using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Score : MonoBehaviour
    {
        public static int finalTime, finalScore;
        static int now = 0;
        public static Stopwatch timer = new(true);
        public static int Now => now;
        public static int Time() => timer.Second();
        public static void StopTimer() { timer.Stop(); finalTime = Time(); finalScore = now; }
        public static void Add(int amount) => now += amount;
    }
}
