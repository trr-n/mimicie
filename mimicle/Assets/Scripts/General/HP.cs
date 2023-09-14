using UnityEngine;

namespace Self.Game
{
    public class HP : MonoBehaviour
    {
        [SerializeField]
        int max = 100;

        [SerializeField]
        int now;

        /// <summary>
        /// 最大HP
        /// </summary>
        public int Max => max;

        /// <summary>
        /// 現在HP
        /// </summary>
        public int Now => now;

        /// <summary>
        /// HPが0か
        /// </summary>
        public bool IsZero => now <= 0;

        /// <summary>
        /// 現在HPの割合
        /// </summary>
        public float Ratio => (float)now / max;

        /// <summary>
        /// 最大HPを設定
        /// </summary>
        public void SetMax(int max) => this.max = max;

        /// <summary>
        /// 最大HPに設定
        /// </summary>
        public void Reset() => now = max;

        /// <summary>
        /// HPをamount分回復
        /// </summary>
        /// <param name="amount"></param>
        public void Healing(int amount)
        {
            now = Mathf.Clamp(now, 0, max);
            now += amount;
            if (now >= max) { Reset(); }
        }

        /// <summary>
        /// HPをamount分減らす
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(int amount)
        {
            now = Mathf.Clamp(now, 0, max);
            now -= amount;
            if (now < 0) { now = 0; }
        }
    }
}