using System;

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