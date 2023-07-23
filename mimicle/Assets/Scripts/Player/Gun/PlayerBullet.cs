using UnityEngine;

namespace Self.Utils
{
    public class PlayerBullet : Bullet
    {
        [SerializeField]
        GameObject effect;

        [SerializeField]
        AudioClip sound;

        new AudioSource audio;

        Vector2 direction;

        void Start()
        {
            direction = transform.right;

            audio = GetComponent<AudioSource>();
        }

        void Update()
        {
            OutOfScreen(gameObject);
            Move(20);
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
                    Numeric.Round(Values.Damage.Player / 3, 0) : Values.Damage.Player);
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                //TODO 着弾時の効果音つける 
                // audio.PlayOneShot(sound);
                effect.Generate(transform.position, Quaternion.identity);

                TakeDamage(info);

                Destroy(gameObject);
            }
        }

    }
}