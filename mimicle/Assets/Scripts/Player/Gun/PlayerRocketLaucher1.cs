using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerRocketLaucher1 : Bullet
    {
        [SerializeField]
        GameObject fireEffect, explosionEffect;

        [SerializeField]
        AudioClip fireSound, explosionSound;

        AudioSource speaker;

        Vector2 direction;
        (float basis, float Reduction) speeds = (15f, 0.2f);

        (float Range, float Mult) damage => (5f, 2f);

        WaveData wdata;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            //発砲音
            speaker.PlayOneShot(fireSound);

            direction = -transform.right;

            wdata = Gobject.GetWithTag<WaveData>(Constant.WaveManager);
        }

        void Update()
        {
            OutOfScreen(gameObject);

            if (speeds.basis >= 0)
            {
                speeds.basis -= speeds.Reduction;
                Move(speeds.basis);
                return;
            }

            Explosion(); // 止まっていたら爆発
        }

        protected override void Move(float speed)
        {
            transform.Translate(speed * direction * Time.deltaTime, Space.World);
        }

        void Explosion()
        {
            var closers = GetClosers();
            if (closers != null)
            {
                Debug.LogWarning("近くの敵はCloser:" + closers.Length);
                foreach (var i in closers)
                {
                    Debug.Log("敵へのダメージ");
                    var distance = this.damage.Range - Vector2.Distance(i.transform.position, transform.position);
                    var enemiesHP = i.GetComponent<HP>();
                    int damage = 0;
                    enemiesHP.Damage(damage = ((int)Numeric.Round(distance * this.damage.Mult, 0)));
                    print("deal: " + damage);
                }
            }
            else
            {
                Debug.LogWarning("近くに敵は居なかった！");
            }

            // エフェクト生成
            explosionEffect.Generate(transform.position);

            Destroy(gameObject);
        }

        /// <summary>
        /// damage.Rangeの範囲内かつHPが残っている敵を取得
        /// </summary>
        GameObject[] GetClosers()
        {
            List<GameObject> enemies = new();
            //enemies.Add(GameObject.FindGameObjectsWithTag(Constant.Enemy));
            var enemiesAry = GameObject.FindGameObjectsWithTag(Constant.Enemy);
            //foreach (var i in enemiesAry)
            //{
            //    enemies.Add(i.gameObject);
            //}

            // ウェーブ3 最終ウェーブ
            if (wdata.Now == 2)
            {
                //enemies.Add(GameObject.FindGameObjectWithTag(Constant.Boss));
                //enemiesAry = GameObject.FindGameObjectsWithTag(Constant.Boss);
            }

            if (enemiesAry == null)//(enemies is null || enemies.Count <= 0)
            {
                return null;
            }

            //print("enemies: " + enemies.Count);
            print("enemiesAry: " + enemiesAry.Length);

            //FIXME 爆発する瞬間にエラー
            var closers = (
                from enemy in enemiesAry//enemies
                    // damage.Range の範囲内
                where Vector2.Distance(enemy.transform.position, transform.position) < damage.Range
                // HPが1以上
                where enemy.GetComponent<HP>().Now >= 1
                select enemy
            )
            .ToArray();

            return closers;
        }

        float GetDistance(GameObject e) => Vector2.Distance(e.transform.position, transform.position);

        protected override void TakeDamage(Collision2D info)
        {
            explosionEffect.Generate(transform.position);
            info.Get<HP>().Damage(Constant.Damage.PlayerRocket);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                TakeDamage(info);
            }
        }
    }
}