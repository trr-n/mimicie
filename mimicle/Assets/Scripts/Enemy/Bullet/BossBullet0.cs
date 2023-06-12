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

        void Start()
        {
            player = gobject.Find(constant.Player).transform;
            direction = transform.position - player.position;
        }

        void FixedUpdate()
        {
            if (gameObject.IsExist())
            {
                speed *= 1.2f;
            }
            Move(speed);
        }
        void Update()
        {
            var distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance <= 10)
            {
                // 近づいたら爆発して被爆
            }
        }

        protected override void Move(float speed)
        {
            transform.Translate(-direction * speed * Time.deltaTime);
        }

        protected override void Attack(Collision2D info)
        {
            var hp = info.Get<HP>();
            var dmgAmount = ((int)numeric.Round(hp.Now / 50, 0));
            $"dmg: {dmgAmount}".show();
            hp.Damage(dmgAmount);

            gameObject.Remove();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
            // if (info.gameObject.TryGetComponent<HP>(out var hp))
            {
                // "hit bullet0".show();
                // var hp = info.Get<HP>();
                // var dmgAmount = ((int)numeric.Round(hp.Ratio / 10, 0));
                // hp.Damage(dmgAmount);

                // gameObject.Remove();
                Attack(info);
            }

            // if (info.Compare(constant.Bullet))
            // {
            //     }
            // }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(constant.Safety))
            {
                gameObject.Remove();
            }
        }

        protected override void Attack() {; }
    }
}