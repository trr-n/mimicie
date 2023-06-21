﻿using System;

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
            switch (style)
            {
                case Style.Standard:
                    return $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                case Style.Rebirth:
                    return $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
                default:
                    throw new Exception();
            }
        }

        public static string Time(Style style = Style.Standard)
        {
            switch (style)
            {
                case Style.Standard:
                    return $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
                case Style.Rebirth:
                    return $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}";
                default:
                    throw new Exception();
            }
        }

        public static float r(int digits = 0) => MathF.Round(UnityEngine.Time.time, digits);
    }
}