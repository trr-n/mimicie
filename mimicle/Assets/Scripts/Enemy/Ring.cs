using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class Ring : Enemy
    {
        /// <summary>
        /// 速度
        /// </summary>
        (float rotate, float move) speed = (1f, 1f);

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
            // transform.Translate(direction * speed.move, Space.World);
            transform.Rotate(Vector3.forward * speed.rotate * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
            {
                info.Get<HP>().Damage(Values.Damage.Ring);
            }
        }
    }
}