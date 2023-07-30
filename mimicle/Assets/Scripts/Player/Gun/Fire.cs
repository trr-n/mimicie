using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utility;

namespace Self
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject[] bulletObjs = new GameObject[3];

        // [SerializeField]
        // AudioClip[] fireSounds;

        [SerializeField]
        Ammo ammo;

        AudioSource speaker;

        void Start()
        {
            speaker = this.GetComponent<AudioSource>();
            ammo = this.GetComponent<Ammo>();
        }

        public void Shot(int activeGrade = 0)
        {
            ammo.Reduce();
            try
            {
                //FIXME 
                // speaker.PlayOneShot(fireSounds[activeGrade]);
            }
            catch (System.NullReferenceException e) { print("raise error"); }

            bulletObjs[activeGrade].Generate(transform.position, Quaternion.Euler(0, 0, 180));
        }
    }
}
