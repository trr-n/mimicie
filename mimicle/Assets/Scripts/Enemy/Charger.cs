using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public sealed class Charger : Enemy
    {
        /// <summary>
        /// chargerのHP
        /// </summary>
        HP chargerHP;

        SpriteRenderer sr;

        /// <summary>
        /// 加速比
        /// </summary>
        readonly float accelRatio = 1.002f;

        enum MovingType { Straight, Circular, Stiffly }
        /// <summary>
        /// 移動タイプ
        /// </summary>
        MovingType style = MovingType.Straight;

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

            style = (MovingType)Rand.Int(max: style.GetEnumLength());
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            Move();
            Left(gameObject);

            if (chargerHP.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Constant.Point.Charger);
            }
        }

        protected override void Move()
        {
            switch (style)
            {
                case MovingType.Straight:
                    sr.SetColor(Color.gray);

                    move.x *= accelRatio;
                    transform.Translate(move.x * Time.deltaTime * Vector2.left);
                    break;

                case MovingType.Circular:
                    sr.SetColor(Color.red);

                    cir *= accelRatio;
                    transform.Translate(move.x * Time.deltaTime * Vector2.left);
                    transform.position = new(transform.position.x, cir * Mathf.Sin(Time.time * 10));
                    break;

                case MovingType.Stiffly:
                    sr.SetColor(Color.blue);

                    float border = 4.5f;
                    if (transform.position.y >= border || transform.position.y <= -border)
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
                info.Get<HP>().Damage(Constant.Damage.Charger);
                Score.Add(Constant.Point.RedCharger);

                Destroy(gameObject);
            }
        }
    }
}
