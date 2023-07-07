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
            char[] charaArr = count is null ? new char[randint(2, 16)] : new char[((int)count)];
            for (int i = 0; i < charaArr.Length; i++)
            {
                charaArr[i] = characters[new System.Random().Next(characters.Length)];
            }
            return charaArr.ToString();
        }
    }
}
