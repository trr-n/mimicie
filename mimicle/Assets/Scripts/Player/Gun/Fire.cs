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
        /// <summary>
        /// 選択されている銃
        /// </summary>
        public int Mode => mode;

        int grade = 0;
        /// <summary>
        /// 銃のグレード
        /// </summary>
        public int Grade => grade;

        bool isFiring = false;
        /// <summary>
        /// 撃ってる途中かどうか
        /// </summary>
        public bool IsFiring => isFiring;

        void Start()
        {
            speaker = GetComponent<AudioSource>();
            ammo = GetComponent<Ammo>();
        }

        void Update()
        {
            // 通常弾
            if (Inputs.Down(Constant.ChangeWeapon1)) { mode = 0; }
            // ロケラン
            else if (Inputs.Down(Constant.ChangeWeapon2)) { mode = 1; }
        }

        /// <summary>
        /// 発砲美人
        /// </summary>
        public void Shot(int activeGrade = 0)
        {
            // 射撃中に設定
            isFiring = true;

            // 銃のグレード設定
            grade = activeGrade;

            // 残弾数を減らす
            ammo.Reduce();

            switch (activeGrade)
            {
                case 0:
                case 1:
                    speaker.PlayOneShot(fireSounds[activeGrade]);
                    bulletObjs[activeGrade].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;

                case 2:
                    speaker.PlayOneShot(fireSounds[mode]);
                    bulletObjs[mode].Generate(transform.position, Quaternion.Euler(0, 0, 180));
                    break;
            }
        }
    }
}
