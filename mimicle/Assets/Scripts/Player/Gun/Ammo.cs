using UnityEngine;

namespace Mimical
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
        public void Reload()
        {
            if (IsMax())
                return;
            for (int i = 0; i <= max; i++)
                remain++;
            if (remain >= 10)
                remain = max;
        }
        public void Reduce(int amount = 1) => remain -= amount;
    }
}