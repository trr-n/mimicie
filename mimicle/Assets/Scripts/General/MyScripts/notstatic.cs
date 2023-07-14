using System;

namespace Feather.Utils
{
    public class One
    {
        bool flag;

        /// <summary>actionを一回実行</summary>
        public void RunOnce(Action action)
        {
            if (flag)
            {
                return;
            }
            action();
            flag = true;
        }
    }
}