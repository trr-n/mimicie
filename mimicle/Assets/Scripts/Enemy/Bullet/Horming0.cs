using System.Collections.Generic;
using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class Horming0 : Bullet
    {
        /// <summary>
        /// 現在のレベル、ダメージ
        /// </summary>
        (int level, int damage) current;

        Boss boss;
        GameObject player;

        /// <summary>
        /// プレイヤーのHP
        /// </summary>
        HP playerHp;

        int[] DamagePercentages => new int[] { 0, 7, 9, 11, 15 };
        Color[] colors => new Color[] { Color.white, new(0.21f, 0.98f, 0.4f), new(0.87f, 0.98f, 0.21f), new(0.98f, 0.66f, 0.21f), new(0.98f, 0.21f, 0.6f) };

        SpriteRenderer sr;
        Quaternion rotate;

        // /// <summary>
        // /// 基礎速度
        // /// </summary>
        // float speed = 12f;

        // /// <summary>
        // /// 加速比
        // /// </summary>
        // float accelRatio = 1.002f;
        (float basis, float accel) speed = (12f, 1.002f);

        /// <summary>
        /// プレイヤーに近付いたらTrue
        /// </summary>
        bool close = true;

        /// <summary>
        /// プレイヤーを検知する距離
        /// </summary>
        float detect = 5f;

        void Start()
        {
            boss = GameObject.Find("boss 1").GetComponent<Boss>();
            current.level = boss.ActiveLevel;

            player = GameObject.FindGameObjectWithTag(Constant.Player);
            playerHp = player.GetComponent<HP>();

            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            Move(speed.basis *= speed.accel);
            OutOfScreen(gameObject);
        }

        protected override void Move(float speed)
        {
            switch (current.level)
            {
                case 1:
                    current.damage = Numeric.Percent(playerHp.Now, DamagePercentages[1]);
                    sr.SetColor(colors[1]);
                    break;

                case 2:
                    current.damage = Numeric.Percent(playerHp.Now, DamagePercentages[2]);
                    sr.SetColor(colors[2]);
                    break;

                case 3:
                    current.damage = Numeric.Percent(playerHp.Now, DamagePercentages[3]);
                    sr.SetColor(colors[3]);
                    break;

                case 4:
                    current.damage = Numeric.Percent(playerHp.Now, DamagePercentages[4]);
                    sr.SetColor(colors[4]);
                    break;

                case 0:
                default:
                    current.damage = DamagePercentages[0];
                    sr.SetColor(colors[0]);
                    break;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= detect)
            {
                close = false;
            }

            var dir = player.transform.position - transform.position;
            rotate = close ?
                Quaternion.FromToRotation(Vector2.up, dir) : Quaternion.Euler(0, 0, transform.eulerAngles.z);

            // transform.rotation = new(0, 0, rotate.z, rotate.w);
            transform.SetRotation(z: rotate.z, w: rotate.w);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(current.damage);
            Score.Add(DamagePercentages[4] * -2);
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
                Destroy(gameObject);
            }
        }
    }
}
