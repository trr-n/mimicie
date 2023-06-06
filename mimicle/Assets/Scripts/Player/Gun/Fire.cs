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
        AudioClip se;

        [SerializeField]
        float power;

        Ammo ammo;
        AudioSource speaker;
        float rapid;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        public void Shot()
        {
            var b = bullet.Instance(transform.position, Quaternion.Euler(0, 0, 180));
            speaker.PlayOneShot(se);
            ammo.Reduce();
            var brig = b.GetComponent<Rigidbody2D>();
            brig.velocity += Vector2.right * power * Time.deltaTime;
            if (b.transform.position.x >= 10)
                b.Remove();
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
