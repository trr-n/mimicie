using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class BossBullet0 : Bullet
    {
        [SerializeField]
        GameObject explosionFx;

        Transform player;

        /// <summary>
        /// 移動速度
        /// </summary>
        const float speed = 10;

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector3 direction;

        /// <summary>
        /// 加速比
        /// </summary>
        const float AccelRatio = 1.001f;

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
            speed *= AccelRatio;
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Try<HP>(out var hp);
            // 現在HPの2%のダメージ
            hp.Damage(Numeric.Round(hp.Now / 50));
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
            {
                TakeDamage(info);
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Constant.Safety))
            {
                Destroy(gameObject);
            }
        }
    }
}