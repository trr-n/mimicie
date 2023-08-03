using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        GameObject[] bulletObjs = new GameObject[2];

        [SerializeField]
        AudioClip[] fireSounds;

        [SerializeField]
        Ammo ammo;

        AudioSource speaker;

        int mode = 0;
        public int Mode => mode;

        int grade = 0;
        public int Grade => grade;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        void Update()
        {
            // 通常弾
            if (Inputs.Down(KeyCode.Alpha1))
            {
                mode = 0;
            }

            // ロケラン
            else if (Inputs.Down(KeyCode.Alpha2))
            {
                mode = 1;
            }
        }

        public void Shot(int activeGrade = 0)
        {
            grade = activeGrade;
            ammo.Reduce();

            switch (activeGrade)
            {
                case 0:
                case 1:
                    try
                    {
                        speaker.PlayOneShot(fireSounds[activeGrade]);
                        print("success!");
                    }
                    catch (Exception e) { print(e.Message); }

                    bulletObjs[activeGrade].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
                case 2:
                    try
                    {
                        speaker.PlayOneShot(fireSounds[mode]);
                        print("success!");
                    }
                    catch (Exception e) { print(e.Message); }

                    bulletObjs[mode].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
            }
        }
    }
}
