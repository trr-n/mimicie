using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class PlayerBullet : Bullet
    {
        [SerializeField]
        GameObject effect;

        [SerializeField]
        AudioClip[] hitSounds;

        new AudioSource audio;

        /// <summary>
        /// 弾の進行方向
        /// </summary>
        Vector2 direction;

        /// <summary>
        /// 弾速
        /// </summary>
        readonly float speed = 20;

        void Start()
        {
            direction = transform.right;
            audio = GetComponent<AudioSource>();
        }

        void Update()
        {
            OutOfScreen(gameObject);
            Move(speed);
        }

        protected override void Move(float speed) => transform.Translate(Time.deltaTime * speed * direction);

        protected override void TakeDamage(Collision2D info)
        {
            // HPをもつ敵にあたったら
            if (info.Try(out HP hp))
            {
                var player = Gobject.GetWithTag<Player>(Constant.Player);
                // 当たった相手がボスだったら三分の一のダメージじゃなかったらそのまま
                int amount = info.Contain("boss") ?
                    Numeric.Round(Constant.Damage.Player[player.CurrentGunGrade] / 3, 0) :
                    Constant.Damage.Player[player.CurrentGunGrade];

                hp.Damage(amount);
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            // てきにあたったら
            if (info.Compare(Constant.Enemy))
            {
                // 音をならす
                try { audio.PlayOneShot(hitSounds.Choice3()); }
                catch { }

                // えふぇくとを出す
                effect.Generate(transform.position, Quaternion.identity);

                // ダメージを与える
                TakeDamage(info);

                // じさつ
                Destroy(gameObject);
            }
        }

    }
}