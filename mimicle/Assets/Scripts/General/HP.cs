using UnityEngine;

namespace Self
{
    public class HP : MonoBehaviour
    {
        [SerializeField]
        int max = 100;
        public void SetMax(int maxValue) => max = maxValue;

        [SerializeField]
        int now;

        public int Max => max;
        public int Now => now;

        public bool IsZero => now <= 0;
        public float Ratio => (float)now / max;

        public void Reset() => now = max;

        public void Healing(int amount)
        {
            now = Mathf.Clamp(now, 0, max);
            now += amount;

            if (now >= max)
            {
                Reset();
            }
        }

        public void Damage(int amount)
        {
            now = Mathf.Clamp(now, 0, max);
            now -= amount;

            if (now < 0)
            {
                now = 0;
            }
        }
    }
}