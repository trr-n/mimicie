using System;
using System.Collections.Generic;

namespace Mimical.Extend
{
    public static class Rnd
    {
        public static float randfloat(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static float randint((float min, float max) range) => UnityEngine.Random.Range(range.min, range.max);
        public static int randint(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        public static int randint((int min, int max) range) => UnityEngine.Random.Range(range.min, range.max);
        [Obsolete] public static int range(int max) => randint(max: max);
        [Obsolete] public static float range(float max) => randfloat(max: max);
        public static int ice(object[] arr) => randint(max: arr.Length - 1);
        public static int ice2(this object[] arr) => randint(max: arr.Length - 1);
        public static T ice3<T>(this T[] arr) => arr[randint(max: arr.Length - 1)];
        public static string str(int? count)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] charaArr = count != null ? new char[count.Int()] : new char[randint(2, 16)];
            var r = new System.Random();
            for (int i = 0; i < charaArr.Length; i++) charaArr[i] = characters[r.Next(characters.Length)];
            return charaArr.ToString();
        }
        public static T Pro<T>(Dictionary<T, float> dict)
        {
            var total = 0f;
            foreach (var per in dict)
                total += per.Value;
            var r = Rnd.randfloat(max: total);
            foreach (var _ in dict)
            {
                r -= _.Value;
                if (r <= 0)
                    return _.Key;
            }
            return new List<T>(dict.Keys)[0];
        }
        public static T Pro<T>(Dictionary<T, int> dict) { return Pro(dict); }
        public static List<float> Pro2<T>(Dictionary<T, float> dict)
        // public static T Pro2<T>(Dictionary<T, double> dict)
        {
            float total = 0;
            foreach (var value in dict.Values)
                total += value;
            List<float> percentage = new();
            foreach (var dv in dict.Values)
                percentage.Add(Numeric.Round(dv / total, 3));
            return percentage;
        }
    }
}
