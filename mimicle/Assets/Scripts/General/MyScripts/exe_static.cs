using System;

namespace Self.Utils
{
    public partial class Execute
    {
        public static T Runner<T>(Func<T> func) => func();
    }
}