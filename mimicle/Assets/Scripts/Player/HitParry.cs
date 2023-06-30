using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class HitParry : MonoBehaviour
    {
        [SerializeField]
        AudioClip parryse;

        new AudioSource audio;

        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.EnemyBullet))
            {
                // audio.PlayOneShot(parryse);
                Destroy(info.gameObject);
            }
        }
    }
}
