using System;
using System.Collections;
using UnityEngine;

namespace Self.Utils
{
    public class Runtime
    {
        bool flag0;
        /// <summary>actionを一回実行</summary>
        public void RunOnce(params Action[] actions)
        {
            if (flag0)
            {
                return;
            }

            foreach (var action in actions)
            {
                action();
            }

            flag0 = true;
        }

        public static T RunFunc<T>(Func<T> func) => func();

        readonly static Stopwatch bookingSW = new(true);
        public static void Book(float time, Action action)
        {
            if (bookingSW.sf >= time)
            {
                action();
                bookingSW.Rubbish();
            }
        }
    }
}