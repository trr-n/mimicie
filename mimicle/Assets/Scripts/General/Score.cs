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

        public static int CurrentTime => Numeric.Cutail(timer.sf);

        public static void StopTimer() => timer.Stop();
        public static void StartTimer() => timer.Start();
        public static void RestartTimer() => timer.Restart();
        public static void ResetTimer() => timer.Reset();

        public static void Add(int amount) => now += amount;
    }

    public class ResultData
    {
        public int score;
        public int time;
        public int parryUseCount;
        public int parrySuccessCount;

        public ResultData() { }

        public ResultData(int score, int time, int parryUseCount, int parrySuccessCount)
        {
            this.score = score;
            this.time = time;
            this.parryUseCount = parryUseCount;
            this.parrySuccessCount = parrySuccessCount;
        }
    }
}
