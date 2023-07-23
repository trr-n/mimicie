using UnityEngine;
using Self.Utils;

namespace Self
{
    public class Shuriken : Bullet
    {
        /// <summary>
        /// 移動方向
        /// </summary>
        Vector3 direction;

        /// <summary>
        /// 
        /// </summary>
        (float move, float rotate) speeds = (10, 10);

        TrailRenderer trail;

        SpriteRenderer sr;

        bool teleport;

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
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * speeds.rotate);
        }

        protected override void TakeDamage(Collision2D info)
        {
            // TODO make fx
            info.Get<HP>().Damage(Values.Damage.Shuriken);
            Score.Add(Values.Point.RedShuriken);
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
            }
        }
    }
}