using UnityEngine;
using Self.Utility;

namespace Self
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
        public static void RestartTimer() => timer.Restart();
        public static void ResetTimer() => timer.Reset();

        public static void Add(int amount) => now += amount;
    }

    public record Result
    {
        public int score { get; init; }
        public int time { get; init; }
    }

    public struct ResultData
    {
        public int score;
        public int time;
    }
}
