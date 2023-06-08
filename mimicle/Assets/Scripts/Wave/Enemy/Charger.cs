using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Charger : Enemy
    {
        float speed = 2;

        HP hp;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
        }

        void Update()
        {
            Move();

            Left(gameObject);

            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);

                Score.Add(GameManager.Point.Charger);
            }
        }

        protected override void Move()
        => transform.Translate(Vector2.left * speed * Time.deltaTime);

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
            {
                info.Get<HP>().Damage(GameManager.Dmg.Charger);

                Score.Add(GameManager.Point.RedCharger);

                gameObject.Remove();
            }
        }
    }
}
