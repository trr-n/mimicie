using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Scroll : MonoBehaviour
    {
        [SerializeField]
        GameManager manager;

        [SerializeField]
        Sprite background;

        [SerializeField]
        GameObject[] cores;

        [SerializeField]
        float scrolling = 3;

        float left = 20.48f;

        void Update()
        {
            if (manager.BGScrollable)
                foreach (var i in cores)
                {
                    i.transform.Translate(Vector2.left * scrolling * Time.deltaTime);

                    if (i.transform.position.x <= -left)
                        i.transform.position = new(left, 0);
                }
        }
    }
}
