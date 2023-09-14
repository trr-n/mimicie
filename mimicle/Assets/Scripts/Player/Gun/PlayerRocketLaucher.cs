using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Self.Utils;
using UnityEngine;

namespace Self.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerRocketLaucher : Bullet
    {
        [SerializeField]
        GameObject fireEffect, explosionEffect;

        [SerializeField]
        AudioClip fireSound, explosionSound;

        AudioSource speaker;

        /// <summary>
        /// 方向
        /// </summary>
        Vector2 direction;

        /// <summary>
        /// 基礎速度、減速比
        /// </summary>
        (float basis, float Reduction) speeds = (15f, 0.2f);

        /// <summary>
        /// 爆風サイズ、基礎ダメージ、倍率
        /// </summary>
        readonly (float Range, int basis, float Mult) damage = (Range: 5f, basis: 25, Mult: 5f);

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            speaker.PlayOneShot(fireSound);

            direction = -transform.right;
        }

        void Update()
        {
            OutOfScreen(gameObject);

            // 速度違反していたら減速する
            if (speeds.basis >= 0)
            {
                speeds.basis -= speeds.Reduction;
                Move(speeds.basis);

                return;
            }

            // 止まったら爆発
            Explosion();
        }

        protected override void Move(float speed) => transform.Translate(Time.deltaTime * speed * direction, Space.World);

        void Explosion()
        {
            var closers = GetClosersArr();

            // 爆発範囲にてきがいなければ
            if (closers is null)
            {
                // 爆発して終わりや
                explosionEffect.Generate(transform.position);
                return;
            }

            // 爆発範囲におったら
            foreach (var enemy in closers)
            {
                // 敵との距離を計測
                float distance = damage.Range - Vector3.Distance(enemy.transform.position, transform.position);

                // 与ダメージ計算
                int damageAmount = Numeric.Cutail(distance * damage.basis * damage.Mult);

                // アターッック！！！
                enemy.GetComponent<HP>().Damage(Mathf.Clamp(damageAmount, 1, 500));
            }

            // 爆発エフェクト生成
            explosionEffect.Generate(transform.position);

            // じけつ
            Destroy(gameObject);
        }

        /// <summary>
        /// 爆発範囲にいる敵を取得
        /// </summary>
        GameObject[] GetClosersArr()
        {
            GameObject[] enemies = Gobject.Finds(Constant.Enemy);

            // 近くに敵がいなかったら北区に帰宅
            if (enemies is null) { return null; }

            var closers =
                from enemy in enemies
                where Vector2.Distance(enemy.transform.position, transform.position) < damage.Range
                where enemy.GetComponent<HP>()
                select enemy;

            return closers.ToArray();
        }

        protected override void TakeDamage(Collision2D info)
        {
            // えふぇくとセイセイ
            explosionEffect.Generate(transform.position);

            // ダメージあげる♡
            info.Get<HP>().Damage(Constant.Damage.PlayerRocket);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy)) { TakeDamage(info); }
        }
    }
}