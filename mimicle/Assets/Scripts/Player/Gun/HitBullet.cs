using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    [RequireComponent(typeof(AudioSource))]
    public class HitBullet : MonoBehaviour
    {
        [SerializeField]
        GameObject fx;

        [SerializeField]
        AudioClip[] se;

        AudioSource spk;
        // Rigidbody2D rb;

        void Start()
        {
            // rb = GetComponent<Rigidbody2D>();
            // rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            spk = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Enemy))
            {
                info.gameObject.TryGetComponent<HP>(out var hp);
                if (hp.Max <= 2000)
                    hp.Damage(Values.Damage.Player);
                else
                    hp.Damage(Values.Damage.Player / 5);
                // spk.PlayOneShot(se[0]);
                fx.Instance(transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(constant.Safety))
            {
                // spk.PlayOneShot(se[1]);
                Destroy(gameObject);
            }
        }
    }
}