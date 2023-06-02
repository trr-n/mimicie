using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Charger : Enemy
    {
        float speed = 2;

        HP hp;

        void Awake()
        {
            hp = GetComponent<HP>();
            hp.SetMax();
        }

        void Update()
        {
            Move(Vector2.left, speed);
            // Leave(this.gameObject, hp);

            if (hp.IsZero)
            {
                gameObject.Remove();
            }
        }
    }
}
