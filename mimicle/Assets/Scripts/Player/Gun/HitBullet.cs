using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    [RequireComponent(typeof(AudioSource))]
    public class HitBullet : MonoBehaviour
    {
        [SerializeField]
        GameObject fx;
        [SerializeField]
        AudioClip se;

        AudioSource spk;

        void Start()
        {
            spk = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Enemy))
            {
                info.Try<HP>(out var hp);
                hp.Damage(info.gameObject.name.Contains("boss") ? Numeric.Round(Values.Damage.Player / 3, 0) : Values.Damage.Player);
                //TODO 着弾時の効果音つける 
                // spk.PlayOneShot(se[0]);
                fx.Generate(transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Constant.Safety))
            {
                // spk.PlayOneShot(se[1]);
                Destroy(gameObject);
            }
        }
    }
}