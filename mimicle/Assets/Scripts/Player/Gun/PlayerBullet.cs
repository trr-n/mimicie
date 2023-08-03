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
        /// 進行方向
        /// </summary>
        Vector2 direction;

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

        protected override void Move(float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            if (info.Try<HP>(out var hp))
            {
                // Player player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<Player>();
                Player player = Gobject.GetWithTag<Player>(Constant.Player);
                hp.Damage(info.gameObject.name.Contains("boss") ?
                    Numeric.Round(Constant.Damage.Player[player.CurrentGunGrade] / 3, 0) :
                    Constant.Damage.Player[player.CurrentGunGrade]
                );
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                try
                {
                    audio.PlayOneShot(hitSounds.Choice3());
                }
                catch { }
                effect.Generate(transform.position, Quaternion.identity);

                TakeDamage(info);
                Destroy(gameObject);
            }
        }

    }
}