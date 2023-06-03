using System;
using UnityEngine;

namespace Mimical.Extend
{
    public enum Style
    {
        Standard, Rebirth
    }

    public static class time
    {
        public static string Date(Style style = Style.Standard)
        {
            var now = DateTime.Now;
            switch (style)
            {
                case Style.Standard:
                    return $"{now.Year}/{now.Month}/{now.Day}";
                case Style.Rebirth:
                    return $"{now.Day}/{now.Month}/{now.Year}";
                default:
                    throw new Exception();
            }
        }

        public static string Time(Style style = Style.Standard)
        {
            var now = DateTime.Now;
            switch (style)
            {
                case Style.Standard:
                    return $"{now.Hour}:{now.Minute}:{now.Second}";
                case Style.Rebirth:
                    return $"{now.Second}:{now.Minute}:{now.Hour}";
                default:
                    throw new Exception();
            }
        }

        public static float r(int digits = 0) => MathF.Round(UnityEngine.Time.time, digits);
    }
}