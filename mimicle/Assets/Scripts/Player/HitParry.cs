using UnityEngine;
using Self.Utils;

namespace Self.Game
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
                info.Destroy();
            }
        }
    }
}
