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
                //FIXME 音を鳴らすとエラー
                // speaker.PlayOneShot(fireSounds[activeGrade]);
            }
            catch (System.NullReferenceException e) { throw e; }

            switch (activeGrade)
            {
                case 0:
                case 1:
                    bulletObjs[activeGrade].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
                case 2:
                    // bulletObjs[Lottery.Weighted(0, 10, 8)].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    bulletObjs.Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    // bulletObjs[1].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
            }
        }
    }
}
