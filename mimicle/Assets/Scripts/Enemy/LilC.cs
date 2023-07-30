using System;
using UnityEngine;
using Self.Utility;
using DG.Tweening;

namespace Self
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
        /// 
        /// </summary>
        Vector2 direction;

        /// <summary>
        /// 連射用
        /// </summary>
        (float Span, Stopwatch sw) rapid = (2, new(true));

        /// <summary>
        /// 死亡処理
        /// </summary>
        Special dead = new();

        void Start()
        {
            lilcHP = GetComponent<HP>();
            lilcHP.SetMax();

            Move();
        }

        void Update()
        {
            Left(gameObject);

            if (rapid.sw.sf >= rapid.Span)
            {
                bullet.Generate(transform.position + new Vector3(-0.75f, 0), Quaternion.identity);
                rapid.sw.Restart();
            }

            if (lilcHP.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Values.Point.LilC);

                dead.Runner(() =>
                {
                    fx.Generate(transform.position);

                    GameObject.FindGameObjectWithTag(Constant.Player).TryGetComponent<HP>(out var playerHp);
                    playerHp.Healing(((int)MathF.Round((playerHp.Max - playerHp.Now) / 2, 0)));
                });
            }
        }

        protected override void Move()
        {
            firstPosition = new(Rnd.Int(3, 6), transform.position.y);
            direction = firstPosition - (Vector2)transform.position;
            transform.DOMove(firstPosition, 10).SetEase(Ease.OutCubic);
        }
    }
}
