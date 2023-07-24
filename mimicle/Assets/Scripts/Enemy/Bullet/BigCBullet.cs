using UnityEngine;
using Self.Utils;

namespace Self
{
    public class BigCBullet : Bullet
    {
        [SerializeField]
        GameObject effect;

        GameObject playerObj;

        /// <summary>
        /// 移動速度
        /// </summary>
        (float basis, float Reduct) speed = (5, 0.03f);

        /// <summary>
        /// 弾の生成座標
        /// </summary>
        Vector2 SpawnedPosition;

        /// <summary>
        /// 進行方向
        /// </summary>
        Vector2 direction;

        /// <summary>
        /// 爆発処理
        /// </summary>
        (float Range, float DmgMult) explosion = (2f, 1.5f);

        void Start()
        {
            direction = -transform.right;
            SpawnedPosition = transform.position;
            playerObj = Gobject.Find(Constant.Player);
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
            Gizmos.color = Color.HSVToRGB(Time.unscaledTime % 1, 1, 1);
            Gizmos.DrawWireSphere(transform.position, explosion.Range);
        }

        void ExploseDamage()
        {
            effect.Generate(transform.position);

            float distance = Vector2.Distance(transform.position, playerObj.transform.position);
            if (distance <= explosion.Range)
            {
                // ダメージ量 = (爆発範囲 - 距離) * ダメージ倍率
                float hitPoint = explosion.Range - distance;
                int damageAmount = ((int)Numeric.Round(hitPoint * explosion.DmgMult));
                playerObj.GetComponent<HP>().Damage(damageAmount);
            }

            Score.Add(Values.Point.RedBigCBullet);
            Destroy(gameObject);
        }

        protected override void Move(float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
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
