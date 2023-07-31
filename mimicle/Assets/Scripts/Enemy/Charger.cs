using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utility;
using DG.Tweening;

namespace Self
{
    public sealed class Charger : Enemy
    {
        /// <summary>
        /// chargerのHP
        /// </summary>
        HP chargerHP;

        new BoxCollider2D collider;
        new SpriteRenderer renderer;

        /// <summary>
        /// 加速比
        /// </summary>
        float accelRatio = 1.002f;

        enum MovingType { Straight, Circular, Stiffly }
        /// <summary>
        /// 移動タイプ
        /// </summary>
        MovingType style = MovingType.Straight;

        /// <summary>
        /// 初期座標
        /// </summary>
        Vector3 SpawnPos;

        /// <summary>
        /// 移動
        /// </summary>
        (float x, float y) move = (5, 10);

        /// <summary>
        /// 円移動パターンの基礎速度
        /// </summary>
        float cir = 1f;

        void Start()
        {
            chargerHP = GetComponent<HP>();
            chargerHP.Reset();

            collider = GetComponent<BoxCollider2D>();
            style = (MovingType)Rnd.Int(max: style.GetEnumLength());
            SpawnPos = transform.position;
            renderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Move();
            Left(gameObject);

            if (chargerHP.IsZero)
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
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParrying)
            {
                info.Get<HP>().Damage(Values.Damage.Charger);
                Score.Add(Values.Point.RedCharger);
                Destroy(gameObject);
            }
        }
    }
}
