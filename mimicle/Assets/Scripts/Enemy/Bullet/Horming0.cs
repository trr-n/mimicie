using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class Horming0 : Bullet
    {
        /// <summary>
        /// ダメージ量
        /// </summary>
        int damageAmount = 0;

        Boss boss;
        GameObject player;

        /// <summary>
        /// プレイヤーのHP
        /// </summary>
        HP playerHp;

        readonly Color[] colors = new Color[] {
            Color.white,
            new(0.21f, 0.98f, 0.4f),
            new(0.87f, 0.98f, 0.21f),
            new(0.98f, 0.66f, 0.21f),
            new(0.98f, 0.21f, 0.6f)
        };

        SpriteRenderer sr;
        Quaternion rotate;

        (float basis, float accel) speed = (12f, 1.002f);

        /// <summary>
        /// プレイヤーに近付いたらTrue
        /// </summary>
        bool isClose = true;

        /// <summary>
        /// プレイヤーを検知する距離
        /// </summary>
        readonly float[] detects = { 5f, 4f, 3.5f, 3f, 2.2f };

        int hitCount = 0;
        readonly int[] DestroyHitCount = { 1, 2, 3, 3, 4 };

        void Start()
        {
            // boss = GameObject.Find("boss 1").GetComponent<Boss>();
            boss = Gobject.GetWithName<Boss>("boss 1");

            player = GameObject.FindGameObjectWithTag(Constant.Player);
            playerHp = player.GetComponent<HP>();

            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Move(speed.basis *= speed.accel);
            OutOfScreen(gameObject);
        }

        void Set(int level)
        {
            damageAmount = Numeric.Percent(playerHp.Now, Constant.Damage.Horming[level]);
            sr.SetColor(colors[level]);
        }

        protected override void Move(float speed)
        {
            switch (boss.CurrentActiveLevel)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    Set(boss.CurrentActiveLevel);
                    break;
                default:
                    throw new System.IndexOutOfRangeException();
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= detects[boss.CurrentActiveLevel])
            {
                isClose = false;
            }

            Vector3 dir = player.transform.position - transform.position;
            rotate = isClose ?
                Quaternion.FromToRotation(Vector2.up, dir) : Quaternion.Euler(0, 0, transform.eulerAngles.z);

            transform.SetRotation(z: rotate.z, w: rotate.w);
            transform.Translate(Time.deltaTime * speed * Vector2.up);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(damageAmount);
            // 現在のダメージ量の2倍スコア減らす
            Score.Add(-(Constant.Damage.Horming[4] * 2));

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
                Destroy(info.gameObject);

                if (hitCount > DestroyHitCount[boss.CurrentActiveLevel])
                {
                    Destroy(gameObject);
                    return;
                }

                hitCount++;
            }
        }
    }
}
