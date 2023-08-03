using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace Self.Utils
{
    public enum StopwatchFormat { H, h, Hour, hour, M, m, Minute, minute, S, s, Second, second, MS, ms }

    public sealed class Stopwatch
    {
        SystemStopwatch sw;

        public Stopwatch() => sw = new();
        public Stopwatch(bool autoStart)
        {
            sw = new();
            sw.Start();
        }

        public void Start() => sw.Start();
        public static void Start(params Stopwatch[] sw)
        {
            foreach (var i in sw)
            {
                i.Start();
            }
        }
        public void Stop() => sw.Stop();
        public void Restart() => sw.Restart();
        public void Reset() => sw.Reset();
        public bool IsRunning() => sw.IsRunning;
        public bool isRunning => sw.IsRunning;
        public static void Rubbish(params Stopwatch[] sw) { foreach (var i in sw) i.Rubbish(); }
        public void Rubbish() { sw.Stop(); sw = null; }

        public int h => sw.Elapsed.Hours;
        public float hf => Numeric.Round((float)sw.Elapsed.TotalHours, 6);
        public int Hour() => sw.Elapsed.Hours;
        public float HourF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalHours, digit);

        public int m => sw.Elapsed.Minutes;
        public float mf => Numeric.Round((float)sw.Elapsed.TotalMinutes, 6);
        public int Minute() => sw.Elapsed.Minutes;
        public float MinuteF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMinutes, digit);

        public int s => sw.Elapsed.Seconds;
        public float sf => Numeric.Round((float)sw.Elapsed.TotalSeconds, 6);
        public int Second() => sw.Elapsed.Seconds;
        public float SecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalSeconds, digit);

        public int ms => sw.Elapsed.Milliseconds;
        public float msf => Numeric.Round((float)sw.Elapsed.TotalMilliseconds, 6);
        public int MSecond() => sw.Elapsed.Milliseconds;
        public float MSecondF(int digit = 6) => Numeric.Round((float)sw.Elapsed.TotalMilliseconds, digit);

        public string Spent() => sw.Elapsed.ToString();
        public int Spent(StopwatchFormat style)
        {
            switch (style)
            {
                case StopwatchFormat.H:
                case StopwatchFormat.h:
                case StopwatchFormat.Hour:
                case StopwatchFormat.hour:
                    return Hour();
                case StopwatchFormat.M:
                case StopwatchFormat.m:
                case StopwatchFormat.Minute:
                case StopwatchFormat.minute:
                    return Minute();
                case StopwatchFormat.S:
                case StopwatchFormat.s:
                case StopwatchFormat.Second:
                case StopwatchFormat.second:
                    return Second();
                case StopwatchFormat.MS:
                case StopwatchFormat.ms:
                    return MSecond();
                default:
                    return -1;
            }
        }
        public float SpentF(StopwatchFormat style)
        {
            switch (style)
            {
                case StopwatchFormat.H:
                case StopwatchFormat.h:
                case StopwatchFormat.Hour:
                case StopwatchFormat.hour:
                    return HourF();
                case StopwatchFormat.M:
                case StopwatchFormat.m:
                case StopwatchFormat.Minute:
                case StopwatchFormat.minute:
                    return MinuteF();
                case StopwatchFormat.S:
                case StopwatchFormat.s:
                case StopwatchFormat.Second:
                case StopwatchFormat.second:
                    return SecondF();
                case StopwatchFormat.MS:
                case StopwatchFormat.ms:
                    return MSecondF();
                default:
                    return -1f;
            }
        }
    }
}
