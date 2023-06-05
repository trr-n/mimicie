using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mimical.Extend
{
    public class Script
    {
        public static void print(object msg) => UnityEngine.Debug.Log(msg);

        public static GameObject Ins(GameObject obj, Vector3 v3, Quaternion quaternion)
        => MonoBehaviour.Instantiate(obj, v3, quaternion);

        public void FollowCursor(Transform tt, float z = 0)
        {
            var cur = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cur.z = z;
            tt.position = cur;
        }

        public void FollowCursorRay(Transform tt, float z = 0)
        {
            var cur = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            tt.position = cur.direction;
        }

        public void GetObject(Transform transform, int click = 0)
        {
            if (Input.GetMouseButtonDown(click))
            {
                Vector3 origin = transform.position, direction = new(100, 0, 0);
                RaycastHit2D hit = Physics2D.Raycast(origin, direction);
                if (hit.collider)
                {
                    var position = hit.collider.gameObject.transform.position;
                    $"position:{position}".show();
                }
            }
        }

        public static IEnumerator Animation(
            Sprite[] sprites, SpriteRenderer sr, float animeSpan = 0.5f)
        {
            int i = 0;
            while (true)
            {
                i = i >= sprites.Length - 1 ? 0 : i + 1;
                sr.sprite = sprites[i];
                yield return new WaitForSeconds(animeSpan);
            }
        }
    }
}
