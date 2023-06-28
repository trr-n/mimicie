using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public sealed class Charger : Enemy
    {
        HP hp;
        BoxCollider2D col;
        // float xx = 8.9f, yy = 5f;
        float speed = 2;
        float accelRatio = 1.0025f;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            col = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            speed *= accelRatio;
            Move();
            Left(gameObject);
            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Values.Point.Charger);
            }
        }

        protected override void Move()
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
            {
                info.Get<HP>().Damage(Values.Damage.Charger);
                Score.Add(Values.Point.RedCharger);
                Destroy(gameObject);
            }
        }
    }
}
