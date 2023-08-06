using System;
using UnityEngine;
using Self.Utils;
using DG.Tweening;

namespace Self.Game
{
    public sealed class LilC : Enemy
    {
        [SerializeField]
        GameObject bullet, fx;

        /// <summary>
        /// little cleverのHP
        /// </summary>
        HP lilcHP;

        /// <summary>
        /// 定位置
        /// </summary>
        Vector2 firstPosition;

        /// <summary>
        /// 連射用
        /// </summary>
        (float Span, Stopwatch sw) rapid = (2, new(true));

        /// <summary>
        /// 死亡処理
        /// </summary>
        readonly Runner dead = new();

        readonly Vector3 Offset = new(-0.75f, 0);

        void Start()
        {
            lilcHP = GetComponent<HP>();
            lilcHP.SetMax(500);
            lilcHP.Reset();

            Move();
        }

        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            Left(gameObject);

            if (rapid.sw.sf >= rapid.Span)
            {
                bullet.Generate(transform.position + Offset, Quaternion.identity);
                rapid.sw.Restart();
            }

            if (lilcHP.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Constant.Point.LilC);

                dead.RunOnce(() =>
                {
                    fx.Generate(transform.position);

                    HP playerHp = Gobject.GetWithTag<HP>(Constant.Player);
                    int amount = Numeric.Round((playerHp.Max - playerHp.Now) / 2, 0);
                    playerHp.Healing(amount);
                });
            }
        }

        protected override void Move()
        {
            firstPosition = new(Rand.Int(3, 6), transform.position.y);
            transform.DOMove(firstPosition, 10).SetEase(Ease.OutCubic);
        }
    }
}
