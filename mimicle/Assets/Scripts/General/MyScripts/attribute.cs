using System;

namespace Self.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public class WarningAttribute : Attribute
    {
        string warningPoint;

        public WarningAttribute(string warningPoint)
        {
            this.warningPoint = warningPoint;
        }
    }
}