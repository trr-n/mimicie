using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Wave : MonoBehaviour
    {
        [SerializeField]
        int max;
        public int Max => max;

        [SerializeField]
        int now = 0;
        public int Now => now;

        public float Progress => now / max;

        void Start()
        {
            Reset();
        }

        void Starting(int wave)
        {
            // ウェーブスタート
        }

        void Next() => now++;

        void Reset() => now = 0;
    }
}
