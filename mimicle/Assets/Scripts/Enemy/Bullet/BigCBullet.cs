using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class BigCBullet : Bullet
    {
        [SerializeField]
        GameObject fx;

        GameObject player;

        /// <summary>
        /// 移動速度
        /// </summary>
        (float basis, float Reduct) speed = (5, 0.01f);

        /// <summary>
        /// 弾の生成座標
        /// </summary>
        Vector2 SpawnedPosition;

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector2 dir;

        /// <summary>
        /// 爆発処理
        /// </summary>
        (float Range, float DamageMagnification) explosion = (2f, 1.5f);

        void Start()
        {
            dir = -transform.right;
            SpawnedPosition = transform.position;

            player = Gobject.Find(Constant.Player);
        }

        void Update()
        {
            if (speed.basis >= 0)
            {
                speed.basis -= speed.Reduct;
                Move(speed.basis);
            }

            else
            {
                ExploseDamage();
            }

            OutOfScreen(gameObject);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.HSVToRGB(Time.time % 1, 1, 1);
            Gizmos.DrawWireSphere(transform.position, explosion.Range);
        }

        void ExploseDamage()
        {
            fx.Generate(transform.position);
            // GetComponent<BoxCollider2D>();

            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= explosion.Range)
            {
                player.GetComponent<HP>().Damage(((int)Numeric.Round(distance * explosion.DamageMagnification)));
            }

            Score.Add(Values.Point.RedBigCBullet);
            Destroy(gameObject);
        }

        protected override void Move(float speed)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.BigC);
            Score.Add(Values.Point.RedBigCBullet);

            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Player) && !info.Get<Parry>().IsParry)
            {
                ExploseDamage();
                TakeDamage(info);
            }

            if (info.Compare(Constant.Bullet))
            {
                ExploseDamage();
                Destroy(gameObject);
            }
        }
    }
}
