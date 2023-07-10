using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cet.Extend;
using DG.Tweening;

namespace Cet
{
    public sealed class Charger : Enemy
    {
        HP hp;
        new BoxCollider2D collider;
        float accelRatio = 1.002f;
        enum MovingType { Straight, Circular, Stiffly }
        MovingType style = MovingType.Straight;
        Vector3 SpawnPos;
        new SpriteRenderer renderer;
        (float x, float y) move = (5, 10);
        float cir = 1f;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            collider = GetComponent<BoxCollider2D>();
            style = (MovingType)Rnd.Int(max: style.GetEnumLength());
            SpawnPos = transform.position;
            renderer = GetComponent<SpriteRenderer>();
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
        {
            switch (style)
            {
                case MovingType.Straight:
                    renderer.SetColor(Color.gray);
                    move.x *= accelRatio;
                    transform.Translate(Vector2.left * move.x * Time.deltaTime);
                    break;
                case MovingType.Circular:
                    renderer.SetColor(Color.red);
                    cir *= accelRatio;
                    transform.Translate(Vector2.left * move.x * Time.deltaTime);
                    transform.position = new(transform.position.x, cir * Mathf.Sin(Time.time * 10));
                    break;
                case MovingType.Stiffly:
                    renderer.SetColor(Color.blue);
                    if (transform.position.y >= 4.5f || transform.position.y <= -4.5f)
                    {
                        move.y *= -1;
                    }
                    transform.Translate(new Vector2(-move.x, move.y) * Time.deltaTime);
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
