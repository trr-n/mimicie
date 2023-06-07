using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

public class HP : MonoBehaviour
{
    [SerializeField]
    int max = 100;
    public int Max => max;

    [SerializeField]
    int now;
    public int Now => now;

    public bool IsZero => now <= 0;

    public float Ratio => (float)now / max;

    public void Healing(int amount)
    {
        now += amount;

        if (now >= max)
        {
            SetMax();
        }
    }

    public void Damage(int amount)
    {
        now -= amount;

        if (now < 0)
        {
            now = 0;
        }
    }

    public void SetMax() => now = max;
}
