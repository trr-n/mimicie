using UnityEngine;

namespace Mimicle
{
    public class Ammo : MonoBehaviour
    {
        [SerializeField]
        int max = 10;

        int remain;
        public int Max => max;
        public int Remain => remain;
        public float Ratio => (float)remain / max;
        public bool IsZero() => remain <= 0;
        public bool IsMax() => remain >= max;
        public void Reduce(int amount = 1)
        {
            remain -= amount;
            if (remain <= 0)
            {
                remain = 0;
            }
        }
        public void Reload()
        {
            if (IsMax())
            {
                return;
            }
            for (int i = 0; i <= max; i++)
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