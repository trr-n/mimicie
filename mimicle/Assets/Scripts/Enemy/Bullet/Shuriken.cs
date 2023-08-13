using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class Shuriken : Bullet
    {
        /// <summary>
        /// 移動方向
        /// </summary>
        Vector3 direction;

        (float move, float rotate) speeds = (10, 10);

        TrailRenderer trail;

        SpriteRenderer sr;

        void Start()
        {
            trail = GetComponent<TrailRenderer>();
            sr = GetComponent<SpriteRenderer>();

            trail.time = 0.2f;
            trail.startWidth = sr.GetSpriteSize().y / 3;
            trail.endWidth = 0f;

            direction = -transform.right;
        }

        void Update()
        {
            Move(speeds.move);
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed)
        {
            transform.Translate(Time.deltaTime * speed * direction, Space.World);
            transform.Rotate(Vector3.forward * speeds.rotate);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Constant.Damage.Shuriken);
            Score.Add(Constant.Point.RedShuriken);
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
            {
                TakeDamage(info);
            }

            if (info.Compare(Constant.PlayerBullet))
            {
                info.Destroy();
            }
        }
    }
}