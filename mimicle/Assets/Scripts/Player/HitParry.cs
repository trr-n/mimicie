using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class HitParry : MonoBehaviour
    {
        [SerializeField]
        AudioClip parrySE;

        new AudioSource audio;
        new Collider2D collider;

        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.EnemyBullet))
            {
                // TODO make se
                // audio.PlayOneShot(parrySE);
                Destroy(info.gameObject);
            }
        }
    }
}
