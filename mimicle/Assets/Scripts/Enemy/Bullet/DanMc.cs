using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self
{
    public class DanMc : Bullet
    {
        void Update()
        {
            Move(speed: 3);
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed) => transform.Translate(Vector2.left * speed * Time.deltaTime);

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.Charger);
            Score.Add(Values.Point.RedCharger);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
            {
                TakeDamage(info);
            }

            if (info.Compare(Constant.Bullet))
            {
                info.Destroy();
                Destroy(gameObject);
            }
        }
    }
}
