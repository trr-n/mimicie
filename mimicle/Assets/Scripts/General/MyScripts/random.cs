using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Extend
{
    public static class random
    {
        public static float randfloat(float min = 0, float max = 0) => UnityEngine.Random.Range(min, max);
        public static int randint(int min = 0, int max = 0) => UnityEngine.Random.Range(min, max);
        [Obsolete] public static int range(int max) => randint(max: max);
        [Obsolete]
        public static float range(float max) => randfloat(max: max);
        public static int ice(object[] arr) => randint(max: arr.Length - 1);
        public static int ice2(this object[] arr) => randint(max: arr.Length - 1);
        [Obsolete("動かない")]
        public static string str(int? count)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] charaArr = count != null ? new char[count.Int()] : new char[randint(2, 16)];
            var r = new System.Random();
            for (int i = 0; i < charaArr.Length; i++)
                charaArr[i] = characters[r.Next(characters.Length)];
            return charaArr.ToString();
        }
    }
}
