using UnityEngine;
using System;
using System.Collections;

namespace Mimical.Extend
{
    public class One
    {
        bool flag;
        public void Once(Action action)
        {
            if (flag)
                return;
            action();
            flag = true;
        }
    }
}