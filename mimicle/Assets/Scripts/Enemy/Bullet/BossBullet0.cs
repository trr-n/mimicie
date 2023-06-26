using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class BossBullet0 : Bullet
    {
        float speed = 10;
        Vector3 direction;
        Transform player;
        float accelRatio = 1.001f;

        void Start()
        {
            player = Gobject.Find(Constant.Player).transform;
            direction = player.position - transform.position;
        }

        void FixedUpdate()
        {
            Move(speed);
        }

        void Update()
        {
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed)
        {
            if (this.gameObject is not null)
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            else Destroy(this);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Try<HP>(out var hp);
            var dmgAmount = ((int)Numeric.Round(hp.Now / 50));
            hp.Damage(dmgAmount);
            gameObject.Remove();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
                TakeDamage(info);
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Constant.Safety))
                gameObject.Remove();
        }
    }
}