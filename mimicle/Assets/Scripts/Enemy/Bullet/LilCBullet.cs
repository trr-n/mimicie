using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class LilCBullet : Bullet
    {
        /// <summary>
        /// 移動速度
        /// </summary>
        float speed = 20;

        Transform playerTransform;

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector2 direction;

        void Start()
        {
            playerTransform = Gobject.Find(Constant.Player).transform;
            direction = playerTransform.position - transform.position;
        }

        void Update()
        {
            Move(speed);
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.LilC);
            Score.Add(Values.Point.RedLilCBullet);
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
                Destroy(gameObject);
            }
        }
    }
}