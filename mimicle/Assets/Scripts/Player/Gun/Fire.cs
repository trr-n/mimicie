using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject[] bullets = new GameObject[3];

        [SerializeField]
        AudioClip se;

        [SerializeField]
        int level = 0;

        Ammo ammo;
        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        public void Shot(int level = 0)
        {
            switch (level)
            {
                // TODO 上下の銃を廃止してメインを強化する
                // ノーマル弾単発
                case 0:
                    bullets[0].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;

                // ロケラン単発
                case 1:
                    break;

                // ロケラン連射
                case 2:
                    break;
            }
            speaker.PlayOneShot(se);
            ammo.Reduce();
        }
    }
}
