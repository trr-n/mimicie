using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Self.Utility;
using UnityEngine;

namespace Self
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerRocketLaucher : Bullet
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

            wdata = GameObject.FindGameObjectWithTag(Constant.WaveManager)
                .GetComponent<WaveData>();
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

            Explosion();
        }

        protected override void Move(float speed)
        {
            transform.Translate(speed * direction * Time.deltaTime, Space.World);
        }

        void Explosion()
        {
            // foreach (var e in GetClosers())
            foreach (var e in GetClosers2())
            {
                var distance = this.damage.Range - Vector2.Distance(e.transform.position, transform.position);
                int damage = 0;
                e.GetComponent<HP>()
                    .Damage(damage = ((int)Numeric.Round(distance * this.damage.Mult, 0)));
                print("deal: " + damage);
            }

            // エフェクト生成
            explosionEffect.Generate(transform.position);

            Destroy(gameObject);
        }

        GameObject[] GetClosers2()
        {
            GameObject[] enemies = Gobject.Finds(Constant.Enemy);

            if (Gobject.Find(Constant.Boss) is not null && wdata.Now == 2)
            {
            }

            if (enemies is null)
            {
                return null;
            }

            var closers = (
                from e in enemies
                where Vector2.Distance(e.transform.position, transform.position) < damage.Range
                where e.GetComponent<HP>().Now > 0
                select e
            );

            return closers.ToArray();
        }

        /// <summary>
        /// damage.Rangeの範囲内かつHPが残っている敵を取得
        /// </summary>
        List<GameObject> GetClosers()
        {
            List<GameObject> enemies = new();
            // enemies.Add(GameObject.FindGameObjectsWithTag(Constant.Enemy));
            foreach (var i in GameObject.FindGameObjectsWithTag(Constant.Enemy))
            {
                enemies.Add(i);
            }

            // 最終ウェーブならボスも対象に追加
            if (wdata.Now == 2 && Gobject.Find(Constant.Manager) is not null)
            {
                enemies.Add(GameObject.FindGameObjectWithTag(Constant.Boss));
            }

            if (enemies is null || enemies.Count <= 0)
            {
                return null;
            }

            print("enemies: " + enemies.Count);

            //FIXME 爆発する瞬間にエラー
            var closers = (
                from enemy in enemies
                    // damage.Range の範囲内
                where Vector2.Distance(enemy.transform.position, transform.position) < damage.Range
                // HPが1以上
                where enemy.Try<HP>().Now > 0
                select enemy
            );

            return closers.ToList();
        }

        protected override void TakeDamage(Collision2D info)
        {
            explosionEffect.Generate(transform.position);
            info.Get<HP>().Damage(Values.Damage.PlayerRocket);
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