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
                info.gameObject.GetComponent<HP>().Damage(Values.Damage.Player);
                // spk.PlayOneShot(se[0]);
                fx.Instance(transform.position, Quaternion.identity);

                gameObject.Remove();
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(constant.Safety))
            {
                // spk.PlayOneShot(se[1]);
                gameObject.Remove();
            }
        }
    }
}