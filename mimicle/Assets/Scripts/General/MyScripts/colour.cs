using UnityEngine;

namespace Self.Utils
{
    public static class Colour
    {
        public static void SetColor(this SpriteRenderer sr, Color color) => sr.color = color;
        public static bool Twins(Color n1, Color n2) => Mathf.Approximately(n1.r, n2.r) && Mathf.Approximately(n1.g, n2.g) && Mathf.Approximately(n1.b, n2.b);
    }
}