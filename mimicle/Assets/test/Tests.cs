using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical.Test
{
    public class Tests : MonoBehaviour
    {
        void _Start()
        {
            print("start lerping: " + Mathf.Lerp(0, 1, 1 * Time.deltaTime));
        }

        void _Update()
        {
            print("update lerping: " + Mathf.Lerp(0, 1, 1 * Time.deltaTime));
        }
    }
}
