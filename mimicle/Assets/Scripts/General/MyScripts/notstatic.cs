using System;
using UnityEngine;

namespace Self.Utility
{
    public class Special
    {
        bool flag;

        /// <summary>actionを一回実行</summary>
        public void Runner(Action action)
        {
            if (flag)
            {
                return;
            }
            action();

            flag = true;
        }

        public T Speed<T>(Type returnType)
        {
            if (returnType != typeof(float) || returnType != typeof(Vector3))
            {
                return default(T);
            }

            return default(T);
        }
    }
}