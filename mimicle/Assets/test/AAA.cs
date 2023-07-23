using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

public class AAA : MonoBehaviour
{
    [SerializeField]
    List<float> n1, n2, n3;

    int[] n = { 1, 20, 100 };

    const int C = 10000;

    KeyValuePair<int, float>[] pairs = { new(1, 1), new(20, 20), new(100, 100) };
    Pair<int, float>[] pairs2 = { new(1, 1), new(20, 20), new(100, 100) };

    void Start()
    {
        for (int i = 0; i < C; i++)
        {
            n1.Add(n[Lottery.Weighted(1, 20, 100)]);
            // n2.Add(Lottery.Weighted(pairs));
            // n3.Add(Lottery.Weighted(pairs2));
        }
    }
}
