using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Self.Utils;

namespace Self.Game
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

        bool isFiring = false;
        public bool IsFiring => isFiring;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        void Update()
        {
            // 通常弾
            if (Inputs.Down(Constant.ChangeWeapon1))
            {
                mode = 0;
            }

            // ロケラン
            else if (Inputs.Down(Constant.ChangeWeapon2))
            {
                mode = 1;
            }
        }

        public void Shot(int activeGrade = 0)
        {
            isFiring = true;

            grade = activeGrade;
            ammo.Reduce();

            switch (activeGrade)
            {
                case 0:
                case 1:
                    speaker.PlayOneShot(fireSounds[activeGrade]);
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
