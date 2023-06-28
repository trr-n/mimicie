using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class BossBullet0 : Bullet
    {
        [SerializeField]
        GameObject explosionFx;

        float speed = 10;
        Vector3 direction;
        Transform player;
        float accelarationRatio = 1.001f;
        bool isHitPlayerBullet;

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
            speed *= accelarationRatio;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Try<HP>(out var hp);
            var dmgAmount = ((int)Numeric.Round(hp.Now / 50));
            hp.Damage(dmgAmount);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player))
                TakeDamage(info);
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Constant.Safety))
                Destroy(gameObject);
        }
    }
}