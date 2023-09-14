using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class Score : MonoBehaviour
    {
        static int now = 0;
        // static int wave;

        readonly static Stopwatch timer = new(true);
        public static Stopwatch RawTimer => timer;

        public static int CurrentScore => now;

        public static float CurrentTime => timer.sf;

        public static void StopTimer() => timer.Stop();
        public static void StartTimer() => timer.Start();
        public static void RestartTimer() => timer.Restart();
        public static void ResetTimer() => timer.Reset();

        public static void Add(int amount) => now += amount;
    }

    public class ResultData
    {
        public int score;
        public float time;
        // public int parryUseCount;
        // public int parrySuccessCount;

        public ResultData(int score, float time)//, int parryUsedCount, int parrySuccessCount)
        {
            this.score = score;
            this.time = time;
            // this.parryUseCount = parryUsedCount;
            // this.parrySuccessCount = parrySuccessCount;
        }
    }
}
