using System;
using System.Text.RegularExpressions;

namespace Self.Utility
{
    public enum TempsFormat { Standard, Rebirth }

    public static class Temps
    {
        // public static string Raw()
        // {
        //     string now = Date() + Time();
        //     string[] _0 = now.Split("/");////new[] { "/", " ", ":" });
        //     string[] nowArray = String.Join(null, _0).Split(":");
        //     return String.Join(null, nowArray);
        // }

        // public static string Raw2 => Typing.Replace((Date() + Time()), "/  :", "");
        public static string Raw2 => (Date() + Time()).ReplaceAtOnce("/  :", "");

        public static string Date(TempsFormat style = TempsFormat.Standard)
        {
            switch (style)
            {
                case TempsFormat.Standard: return $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                case TempsFormat.Rebirth: return $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
                default: throw null;
            }
        }

        public static string Time(TempsFormat style = TempsFormat.Standard)
        {
            switch (style)
            {
                case TempsFormat.Standard: return $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
                case TempsFormat.Rebirth: return $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}";
                default: throw null;
            }
        }
    }
}