using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Self.Utils
{

    public static class Appear
    {
        public static IEnumerator Animation(this Sprite[] sprites, SpriteRenderer sr, float span = 0.5f)
        {
            int count = 0;
            while (true)
            {
                sr.sprite = sprites[count >= sprites.Length - 1 ? 0 : count + 1];
                yield return new WaitForSeconds(span);
            }
        }

        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice3();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;

        public static string SetText(this Text text, object obj) => text.text = obj.ToString();

        public static Color SetColor(this Text text, Color color) => text.color = color;

        public static Color SetAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
        public static Color SetAlpha(this Image image, float alpha) => new(image.color.r, image.color.g, image.color.b, alpha);
        public static Color SetAlpha(this SpriteRenderer sr, float alpha) => new(sr.color.r, sr.color.g, sr.color.b, alpha);

        public static void SetColor(this SpriteRenderer sr, Color color) => sr.color = color;
        public static Color SetColor(this Color c, Color color) => c = color;
        public static Color SetColor(this Color color, float? red = null, float? green = null, float? blue = null, float? alpha = null)
        {
            if (red is null && green is null && blue is null && alpha is null)
            {
                throw new Karappoyanke();
            }
            return new Color(red is null ? color.r : (float)red, green is null ? color.g : (float)green, blue is null ? color.b : (float)blue, alpha is null ? color.a : (float)alpha);
        }

    }
}
