using UnityEngine;

namespace Mimical.Extend
{
    public static class SRenderer
    {
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite2(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice3();
        public static void SetColor(this SpriteRenderer sr, Color color) => sr.color = color;
        public static void SetAlpha(this SpriteRenderer sr, float r = 0, float g = 0, float b = 0, float alpha = 1)
        => sr.color = new(r, g, b, alpha);
    }
}