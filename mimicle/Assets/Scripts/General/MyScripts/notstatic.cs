using System;

namespace Mimicle.Extend
{
    public class One
    {
        bool flag;
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