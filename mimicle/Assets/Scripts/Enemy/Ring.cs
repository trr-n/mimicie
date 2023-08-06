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
        (float rotate, float move) speed = (rotate: 50f, move: 0.5f);

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector3 direction;

        void Start()
        {
            direction = -transform.right;
        }

        void Update()
        {
            Move();
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speed.move * direction, Space.World);
            transform.Rotate(new(0, 0, speed.rotate * Time.deltaTime));
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
            {
                info.Get<HP>().Damage(Constant.Damage.Ring);
            }
        }
    }
}