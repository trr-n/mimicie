using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SLilCBullet : Bullet
    {
        [SerializeField]
        float speed = 1;

        SpiralLilC slilc;
        SpriteRenderer sr;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Move(speed);
            OutOfScreen(gameObject);
        }

        void removal() => gameObject.Remove();

        protected override void Move(float speed)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.SLilC);
            Score.Add(Values.Point.RedSLilCBullet);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
                TakeDamage(info);

            if (info.Compare(Constant.Bullet))
                Destroy(gameObject);
        }
    }
}
