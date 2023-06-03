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
        AudioClip se;

        AudioSource spk;

        void Start()
        {
            spk = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Const.Enemy))
            {
                info.gameObject.GetComponent<HP>().Damage(100);
                spk.PlayOneShot(se);
                fx.Instance(transform.position, Quaternion.identity);
                gameObject.Remove();
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(Const.Safety))
            {
                gameObject.Remove();
            }
        }
    }
}