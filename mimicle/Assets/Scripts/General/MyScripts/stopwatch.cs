// using System;
using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace Mimical.Extend
{
    public enum SW { H, h, Hour, hour, M, m, Minute, minute, S, s, Second, second, MS, ms }
    public class Stopwatch
    {
        SystemStopwatch sw;
        public Stopwatch() => sw = new();
        public void Start() => sw.Start();
        public void Stop() => sw.Stop();
        public void Restart() => sw.Restart();
        public void Reset() => sw.Reset();
        public bool IsOngoing() => sw.IsRunning;
        public int Hour() => sw.Elapsed.Hours;
        public float HourF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalHours, digit);
        public int Minute() => sw.Elapsed.Minutes;
        public float MinuteF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMinutes, digit);
        public int Second() => sw.Elapsed.Seconds;
        public float SecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalSeconds, digit);
        public int MSecond() => sw.Elapsed.Milliseconds;
        public float MSecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMilliseconds, digit);
        public string Spent() => sw.Elapsed.ToString();
        public int Spent(SW style)
        {
            switch (style)
            {
                case SW.H: case SW.h: case SW.Hour: case SW.hour: return Hour();
                case SW.M: case SW.m: case SW.Minute: case SW.minute: return Minute();
                case SW.S: case SW.s: case SW.Second: case SW.second: return Second();
                case SW.MS: case SW.ms: return MSecond();
                default: return 0;
            }
        }
        public float SpentF(SW style)
        {
            switch (style)
            {
                case SW.H: case SW.h: case SW.Hour: case SW.hour: return HourF();
                case SW.M: case SW.m: case SW.Minute: case SW.minute: return MinuteF();
                case SW.S: case SW.s: case SW.Second: case SW.second: return SecondF();
                case SW.MS: case SW.ms: return MSecondF();
                default: return 0f;
            }
        }
    }
}
