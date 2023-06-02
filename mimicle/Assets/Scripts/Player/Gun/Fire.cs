using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;

        [SerializeField]
        AudioClip[] ses;

        [SerializeField]
        float power;

        Ammo ammo;

        new AudioSource audio;
        float rapid;

        void Start()
        {
            audio = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        public void Shot()
        {
            var b = bullet.ins(transform.position, Quaternion.Euler(0, 0, 180));
            ammo.Red();
            audio.PlayOneShot(ses[0]);
            var brig = b.GetComponent<Rigidbody2D>();
            brig.velocity += Vector2.right * power * Time.deltaTime;
        }

        IEnumerator TestFire()
        {
            while (true)
            {
                Shot();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
