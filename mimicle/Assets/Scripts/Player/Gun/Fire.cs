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
        GameObject playerBullet;

        [SerializeField]
        AudioClip se;

        Ammo ammo;
        AudioSource speaker;

        int activeType;
        public int ActiveType => activeType;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        public void Shot(int activeType = 0)
        {
            this.activeType = activeType;
            GameObject bulletObj = null;
            PlayerBullet playerBullet = null;

            switch (activeType)
            {
                case 0:
                    print("0ing");
                    // 弾取得
                    bulletObj = bullets[0].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    // 弾のplayerbullet取得
                    playerBullet = bulletObj.GetComponent<PlayerBullet>();
                    // 弾のグレード設定
                    playerBullet.SetBulletType(activeType);
                    break;

                case 1:
                    print("1ing");
                    break;

                case 2:
                    print("2ing");
                    break;

                default:
                    throw new System.Exception("out of range active type");
            }

            speaker.PlayOneShot(se);
            ammo.Reduce();
        }
    }
}
