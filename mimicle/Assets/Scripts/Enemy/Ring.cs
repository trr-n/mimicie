using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class Ring : Enemy
    {
        /// <summary>
        /// 速度
        /// </summary>
        (float rotate, float move) speed = (rotate: 75f, move: 2f);

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector3 direction;

        void Start()
        {
            direction = Vector2.left;
        }

        void Update()
        {
            Move();
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed.move * direction, Space.World);
            transform.Rotate(Time.deltaTime * speed.rotate * Coordinate.Z);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
            {
                info.Get<HP>().Damage(Constant.Damage.Ring);
            }

            if (info.Compare(Constant.PlayerBullet))
            {
                info.Destroy();
            }
        }
    }
}