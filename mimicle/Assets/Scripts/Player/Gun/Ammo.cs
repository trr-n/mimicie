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

        /// <summary>
        /// 残弾数
        /// </summary>
        public int Remain => remain;

        /// <summary>
        /// 残弾数が0以下で True
        /// </summary>
        public bool IsZero() => remain <= 0;

        /// <summary>
        /// 残弾数が最大値だったら True
        /// </summary>
        public bool IsMax() => remain >= Max;

        /// <summary>
        /// リロード処理
        /// </summary>
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

        /// <summary>
        /// 残弾数を減らす処理
        /// </summary>
        public void Red(int amount = 1) => remain -= amount;
    }
}