using System.Collections.Generic;
using System.Collections.Concurrent;
using Feather.Utils;
using UnityEngine;

namespace Feather
{
    public class Scroll : MonoBehaviour
    {
        // [SerializeField]
        GameManager manager;
        [SerializeField]
        Color[] colors;
        [SerializeField]
        GameObject[] backgrounds;

        float speed = 3;
        Vector2 Spawn => new Vector2(20f, 0);
        bool scroll = false;

        void Awake()
        {
            if (MyScene.Active() == Constant.Main)
            {
                manager = Gobject.Find(Constant.Manager).GetComponent<GameManager>();
            }
        }

        void Start()
        {
            foreach (var i in backgrounds)
            {
                i.GetComponent<SpriteRenderer>().color = colors.Choice3();
            }
        }

        void Update()
        {
            if (MyScene.Active() == Constant.Main)
            {
                scroll = manager.BGScroll;
            }
            else
            {
                scroll = true;
            }

            if (!scroll)
            {
                return;
            }

            foreach (var i in backgrounds)
            {
                i.transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (i.transform.position.x <= -20f)
                {
                    i.transform.position = Spawn;
                    i.GetComponent<SpriteRenderer>().color = colors.Choice3();
                }
            }
        }
    }
}
