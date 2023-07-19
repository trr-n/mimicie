using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feather.Utils;

public class AAA : MonoBehaviour
{
    [SerializeField]
    List<float> numbers;

    int[] n = { 1, 20, 100 };

    KeyValuePair<int, float>[] pairs = { new(1, 1), new(20, 20), new(100, 100) };
    Pair<int, float>[] pairs2 = { new(1, 1), new(20, 20), new(100, 100) };

    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            // numbers.Add(n[Lottery.ChoiceByWeights(1, 20, 100)]);
            // numbers.Add(Lottery.ChoChoi(n, new float[] { 1, 20, 100 }));
            // numbers.Add(Lottery.ChoiceByWeights(pairs));
            numbers.Add(Lottery.ChoiceByWeights(pairs2));
        }
    }
}
