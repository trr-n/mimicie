using UnityEngine;

namespace Self.Utils
{
    public class PlayerBullet : Bullet
    {
        [SerializeField]
        [Range(0, 2)]
        int bulletType;

        [SerializeField]
        Sprite[] bulletSprites = new Sprite[3];

        [SerializeField]
        GameObject effect;
        // GameObject[] hitEffects;

        [SerializeField]
        AudioClip sound;
        // AudioClip[] hitSounds;

        new AudioSource audio;
        SpriteRenderer sr;

        Vector2 direction;

        float speed = 20;

        int currentLevel = 0;
        public int CurrentLevel => currentLevel;

        Fire fire;

        One setSprite = new();

        void Start()
        {
            direction = transform.right;
            audio = GetComponent<AudioSource>();

            fire = GameObject.FindGameObjectWithTag(Constant.Player)
                .transform.GetChild(0).GetComponent<Fire>();

            sr = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            OutOfScreen(gameObject);
            Move(speed);
        }

        void LateUpdate()
        {
            // setSprite.RunOnce(() => SetBulletSprite(currentLevel));
            SetBulletSprite(currentLevel);
        }

        public void SetBulletType(int bulletType) => currentLevel = bulletType;

        void SetBulletSprite(int grade) => sr.sprite = bulletSprites[grade];

        protected override void Move(float speed)
        {
            switch (currentLevel)
            {
                // 直進
                case 0:
                    transform.Translate(direction * speed * Time.deltaTime);
                    break;

                // ろけらん
                case 1:
                    break;

                // ろけらん連射
                case 2:
                    break;
            }
        }

        protected override void TakeDamage(Collision2D info)
        {
            if (info.Try<HP>(out var hp))
            {
                hp.Damage(info.gameObject.name.Contains("boss") ?
                    Numeric.Round(Values.Damage.Player[currentLevel] / 3, 0) : Values.Damage.Player[currentLevel]);
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