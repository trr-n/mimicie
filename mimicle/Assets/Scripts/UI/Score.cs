using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Score : MonoBehaviour
    {
        static int now = 0;
        public int Now => now;

        public static void Add(int amount) => now += amount;
    }
}
