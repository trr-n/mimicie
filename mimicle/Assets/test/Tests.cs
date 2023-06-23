using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical.Test
{
    public class _Tests : MonoBehaviour
    {
        void Start()
        {
            for (int i = 0; i < 12; i++)
            {
                print(i + ":" + IsPrime(i));
                print(i + ":" + Numeric.IsPrime(i) + " by numeric");
            }
        }
        void Update()
        {

        }
        public static bool IsPrime(int num)
        {
            if (num < 2) return false;
            else if (num == 2) return true;
            else if (num % 2 == 0) return false;

            double sqrtNum = Math.Sqrt(num);
            for (int i = 3; i <= sqrtNum; i += 2)
            {
                if (num % i == 0)
                {
                    // 素数ではない
                    return false;
                }
            }

            // 素数である
            return true;
        }
    }
}
