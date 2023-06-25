using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical.Test
{
    public class TestTest : MonoBehaviour
    {
        Dictionary<string, float> dictionary = new(5) { { "kara", 1 }, { "oko", 5 }, { "nana", 20 }, { "kuri", 60 }, { "eru", 60 } };
        List<float> list = new(5) { 2, 10, 40, 120, 120 };
        float[] array = new float[5] { 4, 20, 80, 240, 240 };
        void Start()
        {
            foreach (var i in Rnd.Pro2(dictionary))
                print(i);
        }
    }
}
