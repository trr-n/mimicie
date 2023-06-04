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
        Text nowWaveT;

        [SerializeField]
        Text maxWaveT;

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

        [SerializeField]
        Image reloadImage;

        [SerializeField]
        Player player;

        void Update()
        {
            Text();
            Image();
        }

        void Image()
        {
            // 残りHP
            hpImage.fillAmount = playerHp.Ratio;
            // リロード
            reloadImage.fillAmount = player.ReloadProgress;
        }

        void Text()
        {
            // 経過時間
            timeT.text = time.r().ToString();
            // 今のウェーブ数
            nowWaveT.text = wave.Now.ToString();
            // 最大ウェーブ数
            maxWaveT.text = wave.Max.ToString();
            // 残弾数
            ammoT.text = ammo.Remain.ToString();
            // FPS
            fpsT.text = visual.fps().ToString();
        }
    }
}
