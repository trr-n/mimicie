using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mimical.Extend
{
    public static class Script
    {
        public static IEnumerator Animation(Sprite[] sprites, SpriteRenderer sr, float span = 0.5f)
        {
            int i = 0;
            while (true)
            {
                sr.sprite = sprites[i >= sprites.Length - 1 ? 0 : i + 1];
                yield return new WaitForSeconds(span);
            }
        }
    }
}
