using UnityEngine;
using UnityEngine.UI;

namespace Self.Utils
{
    public static class Colour
    {
        public static Color Transparent => new(0, 0, 0, 0);


        public static bool Twins(Color n1, Color n2) => Mathf.Approximately(n1.r, n2.r) && Mathf.Approximately(n1.g, n2.g) && Mathf.Approximately(n1.b, n2.b);
    }
}