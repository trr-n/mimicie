using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnionEngine.Extend;

namespace UnionEngine
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;
        [SerializeField]
        AudioClip se;

        Ammo ammo;
        AudioSource speaker;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        public void Shot()
        {
            bullet.Instance(transform.position, Quaternion.Euler(0, 0, 180));
            speaker.PlayOneShot(se);
            ammo.Reduce();
        }
    }
}
