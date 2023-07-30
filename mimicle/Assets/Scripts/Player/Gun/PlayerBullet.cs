using UnityEngine;
using Self.Utility;

namespace Self
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

        float speed = 20;

        // Fire fire;

        void Start()
        {
            direction = transform.right;
            // fire = GameObject.FindGameObjectWithTag(Constant.Player)
            //     .transform.GetChild(0).GetComponent<Fire>();

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
                Player player = GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<Player>();
                hp.Damage(info.gameObject.name.Contains("boss") ?
                    Numeric.Round(Values.Damage.Player[player.CurrentGunGrade] / 3, 0) :
                    Values.Damage.Player[player.CurrentGunGrade]
                );
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                try
                {
                    //TODO 着弾時の効果音つける 
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