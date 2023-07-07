using System.Collections.Generic;
using System.Collections.Concurrent;
using Mimical.Extend;
using UnityEngine;

namespace Mimical
{
    public class Scroll : MonoBehaviour
    {
        [SerializeField]
        GameManager manager;
        [SerializeField]
        Color[] colors;
        [SerializeField]
        GameObject[] backgrounds;

        float speed = 3;
        Vector2 Spawn => new Vector2(20f, 0);

        void Start()
        {
            foreach (var i in backgrounds)
            {
                i.GetComponent<SpriteRenderer>().color = colors.ice3();
            }
        }

        void Update()
        {
            if (!manager.BGScroll)
            {
                return;
            }

            foreach (var i in backgrounds)
            {
                i.transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (i.transform.position.x <= -20f)
                {
                    i.transform.position = Spawn;
                    i.GetComponent<SpriteRenderer>().color = colors.ice3();
                }
            }
        }
    }
}
