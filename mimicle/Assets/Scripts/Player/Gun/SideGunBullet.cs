using UnityEngine;
using Self.Utils;

namespace Self
{
    [RequireComponent(typeof(AudioSource))]
    public class SideGunBullet : Bullet
    {
        [SerializeField]
        GameObject effect;

        [SerializeField]
        AudioClip sound;

        AudioSource source;

        /// <summary>
        /// 移動速度
        /// </summary>
        float speed = 15;

        Vector2 direction;

        void Start()
        {
            source = GetComponent<AudioSource>();

            transform.SetEuler(z: -90);
            direction = -transform.right;
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
                hp.Damage(info.gameObject.name.Contains("boss") ?
                    Numeric.Round(Values.Damage.MiniPlayer / 3, 0) : Values.Damage.MiniPlayer);
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            // TODO
            // effect.Generate(transform.position);
            // source.PlayOneShot(sound);

            if (info.Compare(Constant.Enemy))
            {
                TakeDamage(info);
            }
        }
    }
}