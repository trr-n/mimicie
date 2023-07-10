using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using DG.Tweening;

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
        Vector3 SpawnPos;
        new SpriteRenderer renderer;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            collider = GetComponent<BoxCollider2D>();
            style = (Style)Rnd.Int(max: style.GetEnumLength());
            SpawnPos = transform.position;
            renderer = GetComponent<SpriteRenderer>();
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

        One one = new();
        protected override void Move()
        {
            switch (style)
            {
                case Style.straight:
                    renderer.SetColor(Color.gray);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    break;
                case Style.circular:
                    renderer.SetColor(Color.red);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    transform.position = new(transform.position.x, Mathf.Sin(Time.time * 10));
                    break;
                case Style.stiffly:
                    renderer.SetColor(Color.blue);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    transform.position = new(transform.position.x, Mathf.PingPong(Time.time * 5, 4));
                    break;
            }
        }

        void Up()
        {
        }

        void Down()
        {
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
