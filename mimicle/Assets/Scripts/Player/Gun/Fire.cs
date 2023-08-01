using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utility;

namespace Self
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject[] bulletObjs = new GameObject[2];

        // [SerializeField]
        // AudioClip[] fireSounds;

        [SerializeField]
        Ammo ammo;

        AudioSource speaker;

        int mode = 0;
        public int Mode => mode;

        int grade = 0;
        public int Grade => grade;

        void Start()
        {
            speaker = this.GetComponent<AudioSource>();
            ammo = this.GetComponent<Ammo>();
        }

        void Update()
        {
            //TODO playerクラスのRapidSpanをmodeに合わせて変える
            print("mode: " + mode);
            // 通常弾
            if (Feed.Down(KeyCode.Alpha1))
            {
                mode = 0;
            }

            // ロケラン
            else if (Feed.Down(KeyCode.Alpha2))
            {
                mode = 1;
            }
        }

        public void Shot(int activeGrade = 0)
        {
            ammo.Reduce();

            grade = activeGrade;

            switch (activeGrade)
            {
                case 0:
                case 1:
                    bulletObjs[activeGrade].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
                case 2:
                    bulletObjs[mode].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
            }
        }
    }
}
