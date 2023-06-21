using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class BossBullet0 : Bullet
    {
        [SerializeField]
        float speed = 2;

        Vector3 direction;
        Transform player;

        float accelRatio = 1.001f;

        void Start()
        {
            player = gobject.Find(constant.Player).transform;
            direction = player.position - transform.position;
        }

        void FixedUpdate()
        {
            if (gameObject.IsExist())
                speed *= accelRatio;
            Move(speed);
        }
        void Update()
        {
            // var distance = Vector3.Distance(player.transform.position, this.transform.position);
            // if (distance <= 10) {; }
            if (transform.position.x <= -20.48f)
                Destroy(this.gameObject);
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed)
        {
            if (this.gameObject is not null)
                transform.Translate(direction * speed * Time.deltaTime);
            else Destroy(this);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Try<HP>(out var hp);
            var dmgAmount = ((int)numeric.Round(hp.Now / 50));
            hp.Damage(dmgAmount);
            gameObject.Remove();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
                TakeDamage(info);
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(constant.Safety))
                gameObject.Remove();
        }
    }
}