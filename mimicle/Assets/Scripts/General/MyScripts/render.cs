using System.Collections;
using UnityEngine;
using static UnityEngine.Cursor;

namespace Self.Utils
{
    public enum c { hide, show }
    public enum v { inscene = CursorLockMode.Confined, locked = CursorLockMode.Locked, unlocked = CursorLockMode.None }
    public static class Render
    {
        public static float FPS() => Mathf.Floor(1 / Time.deltaTime);
        public static void SetCursorStatus(c status = c.hide, v clock = v.inscene) { visible = status != c.hide; lockState = (CursorLockMode)clock; }

        public static IEnumerator Animation(this Sprite[] sprites, SpriteRenderer sr, float span = 0.5f)
        {
            int i = 0;
            while (true)
            {
                sr.sprite = sprites[i >= sprites.Length - 1 ? 0 : i + 1];
                yield return new WaitForSeconds(span);
            }
        }
        public static bool Compare(this SpriteRenderer sr, Sprite sprite) => sr.sprite == sprite;

        public static void SetSprite(this SpriteRenderer sr, Sprite sprite) => sr.sprite = sprite;
        public static void SetSprite2(this SpriteRenderer sr, Sprite[] sprites) => sr.sprite = sprites.Choice3();

        public static Vector2 GetSpriteSize(this SpriteRenderer sr) => sr.bounds.size;
    }
}
