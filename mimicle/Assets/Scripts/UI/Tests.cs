using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        [SerializeField]
        Text waveT;

        [SerializeField]
        Wave wave;

        [SerializeField]
        Text ammoT;

        [SerializeField]
        Ammo ammo;

        [SerializeField]
        Image hpImage;

        [SerializeField]
        HP playerHp;

        [SerializeField]
        Text fpsT;

        void Update()
        {
            // 経過時間
            timeT.text = time.r().ToString();
            // ウェーブ数
            waveT.text = wave.Now.ToString();
            // 残弾数
            ammoT.text = ammo.Remain.ToString();
            // 残りHP
            hpImage.fillAmount = playerHp.Ratio;
            // FPS
            fpsT.text = visual.fps().ToString();
        }
    }
}
