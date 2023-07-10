using UnityEngine;
using Cet.Extend;

namespace Cet
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
                audio.PlayOneShot(parrySE);
                Destroy(info.gameObject);
            }
        }
    }
}
