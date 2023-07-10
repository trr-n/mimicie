using UnityEngine;

namespace Mimical.Extend
{
    public static class SRenderer
    {
        public static bool Compare(this SpriteRenderer renderer, Sprite sprite) => renderer.sprite == sprite;
        public static void SetSprite(this SpriteRenderer renderer, Sprite sprite) => renderer.sprite = sprite;
        public static void SetSprite2(this SpriteRenderer renderer, Sprite[] sprites) => renderer.sprite = sprites.Choice3();
        public static void SetColor(this SpriteRenderer renderer, Color color) => renderer.color = color;
    }
}