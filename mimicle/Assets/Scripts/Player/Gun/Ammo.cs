using UnityEngine;

namespace Self.Game
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField]
        int max = 10;

        int remain;

        /// <summary>
        /// 最大装弾数
        /// </summary>
        public int Max => max;

        /// <summary>
        /// 残弾数
        /// </summary>
        public int Remain => remain;

        /// <summary>
        /// 残弾数の割合
        /// </summary>
        public float Ratio => (float)remain / max;

        /// <summary>
        /// 残弾数が0ならTrue
        /// </summary>
        public bool IsZero() => remain <= 0;

        /// <summary>
        /// 残弾数がmaxならtrue
        /// </summary>
        public bool IsMax() => remain >= max;

        /// <summary>
        /// 残弾数を amount ずつ減らす
        /// </summary>
        public void Reduce(int amount = 1)
        {
            remain -= amount;

            if (remain <= 0)
            {
                remain = 0;
            }
        }

        /// <summary>
        /// リロード
        /// </summary>
        public void Reload()
        {
            if (IsMax())
            {
                return;
            }

            for (ushort count = 0; count <= max; count++)
            {
                remain = Mathf.Clamp(remain, 0, max);
                remain++;
            }

            if (remain >= 10)
            {
                remain = max;
            }
        }
    }
}