using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField]
        int max = 10;
        public int Max => max;

        int remain;

        public int Remain => remain;

        public bool IsZero() => remain <= 0;

        public bool IsMax() => remain >= max;

        public void Reload()
        {
            if (IsMax())
                return;

            for (int i = 0; i <= max; i++)
            {
                remain++;

                if (remain > max)
                    remain = max;
            }
        }

        public void Reduce(int amount = 1) => remain -= amount;
    }
}