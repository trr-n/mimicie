using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Extend
{
    public static class random
    {
        public static int zero2max(this int max) => UnityEngine.Random.Range(0, max);

        public static float zero2max(this float max) => UnityEngine.Random.Range(0f, max);

        public static int ice(int length) => UnityEngine.Random.Range(0, length - 1);

        public static float randfloat(float min = 0, float max = 0)
         => UnityEngine.Random.Range(min, max);

        public static int randint(int min = 0, int max = 0)
        => UnityEngine.Random.Range(min, max);

        [Obsolete("cannot execute")]
        public static string str(int? count)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            // var charasArr = new char[(int)count];
            char[] charaArr = count != null ?
                new char[count.ToInt()] : new char[randint(2, 16)];
            System.Random random = new();
            for (int i = 0; i < charaArr.Length; i++)
            {
                charaArr[i] = characters[random.Next(characters.Length)];
            }
            return charaArr.ToString();
        }
    }
}
