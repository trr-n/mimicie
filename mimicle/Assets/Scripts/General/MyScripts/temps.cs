using System;
using System.Text.RegularExpressions;

namespace Self.Utils
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

        public static string Raw2 => (Date() + Time()).ReplaceAtOnce("/  :", "");

        public static string Date(TempsFormat style = TempsFormat.Standard)
        {
            return style switch
            {
                TempsFormat.Standard => $"{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}",
                TempsFormat.Rebirth => $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}",
                _ => throw null,
            };
        }

        public static string Time(TempsFormat style = TempsFormat.Standard)
        {
            return style switch
            {
                TempsFormat.Standard => $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}",
                TempsFormat.Rebirth => $"{DateTime.Now.Second}:{DateTime.Now.Minute}:{DateTime.Now.Hour}",
                _ => throw null,
            };
        }
    }
}