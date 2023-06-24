using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Score : MonoBehaviour
    {
        public static Stopwatch timer = new(true);
        static int scoreReductionRatio = 10;
        static int now = 0;
        public static int Now => now;
        public static int finalTime, finalScore;
        public static int Time() => timer.Second();
        public static void StopTimer() => timer.Stop();
        public static void Add(int amount) => now += amount;
        // public static void SetFinal()
        // {
        //     timer.Stop();
        //     finalTime = timer.Spent(SWFormat.Second);
        //     finalScore = now;
        // }
    }
}
