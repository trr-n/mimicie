using UnityEngine;
using Feather.Utils;

namespace Feather
{
    public class Score : MonoBehaviour
    {
        static int now = 0;
        static int wave;
        static Stopwatch timer = new(true);
        public static int Now => now;
        public static int Time => timer.Second();
        public static void StopTimer() => timer.Stop();
        public static void StartTimer() => timer.Start();
        public static void Add(int amount) => now += amount;
    }

    public struct ScoreData
    {
        public int wave;
        public int score;
        public int time;
    }
}
