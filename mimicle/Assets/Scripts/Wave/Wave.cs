using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Wave : MonoBehaviour
    {
        int max = 3;
        public int Max => max;

        [SerializeField]
        int now = 0;
        public int Now => now;

        public float Progress => now / max;

        public void Next() => now++;

        void Reset() => now = 0;

        public void Set(int wave) => now = wave;
    }
}
