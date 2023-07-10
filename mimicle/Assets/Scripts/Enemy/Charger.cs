using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public sealed class Charger : Enemy
    {
        HP hp;
        new BoxCollider2D collider;
        float speed = 5;
        float accelRatio = 1.002f;
        enum Style { straight, circular, stiffly }
        Style style = Style.straight;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            collider = GetComponent<BoxCollider2D>();
            style = (Style)Rnd.Int(max: style.GetEnumLength());
        }

        void Update()
        {
            print(style.GetEnumLength());
            // speed *= accelRatio;
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
            switch (style)
            {
                case Style.straight:
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    break;
                case Style.circular:
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    transform.position = new(transform.position.x, Mathf.Sin(Time.time * 10));
                    break;
                case Style.stiffly:
                    // TODO kakukaku movement
                    break;
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
            {
                info.Get<HP>().Damage(Values.Damage.Charger);
                Score.Add(Values.Point.RedCharger);
                Destroy(gameObject);
            }
        }
    }
}
