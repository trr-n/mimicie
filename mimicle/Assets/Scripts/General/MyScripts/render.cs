using System.Collections;
using UnityEngine;

namespace Self.Utils
{

    public static class Render
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
    }
}
