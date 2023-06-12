using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Charger : Enemy
    {
        HP hp;

        BoxCollider2D col;

        float xx = 8.9f, yy = 5f;

        float speed = 2;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);

            col = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            Move();
            Left(gameObject);

            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Values.Point.Charger);
            }
        }

        protected override void Move()
        => transform.Translate(Vector2.left * speed * Time.deltaTime);

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
            {
                info.Get<HP>().Damage(Values.Damage.Charger);
                Score.Add(Values.Point.RedCharger);

                gameObject.Remove();
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(constant.Safety))
            {
                gameObject.Remove();
                Score.Add(-100);
            }
        }

        protected override void OnBecameInvisible()
        {
            col.isTrigger = true;
        }

        protected override void OnBecameVisible()
        {
            col.isTrigger = false;
        }
    }
}
