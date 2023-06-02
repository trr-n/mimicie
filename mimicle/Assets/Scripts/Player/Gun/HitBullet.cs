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

        new AudioSource audio;

        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(cst.Enemy))
            {
                info.gameObject.GetComponent<HP>().Damage(100);
                audio.PlayOneShot(se);
                fx.Instance(transform.position, Quaternion.identity);
                gameObject.Remove();
            }
        }

        void OnTriggerExit2D(Collider2D info)
        {
            if (info.Compare(cst.Safety))
            {
                gameObject.Remove();
            }
        }
    }
}