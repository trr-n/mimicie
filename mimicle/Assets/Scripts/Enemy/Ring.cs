using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self
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
            // Debug.DrawLine(transform.position, direction * 100, Color.HSVToRGB(Time.time % 1, 1, 1));
            Move();
        }

        protected override void Move()
        {
            transform.Translate(direction * speed.move * Time.deltaTime, Space.World);
            transform.Rotate(new(0, 0, speed.rotate * Time.deltaTime));
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