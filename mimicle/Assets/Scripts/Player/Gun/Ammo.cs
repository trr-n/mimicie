using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("最大装弾数")]
        int Max = 10;

        int remain;

        public int Remain => remain;
        public bool IsZero() => remain <= 0;
        public bool IsMax() => remain >= Max;

        public void Reload()
        {
            if (IsMax())
            {
                return;
            }
            for (int i = 0; i <= Max; i++)
            {
                remain++;
                if (remain > Max)
                {
                    remain = Max;
                }
            }
        }

        public void Red(int amount = 1) => remain -= amount;
    }
}