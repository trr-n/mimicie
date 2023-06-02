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
            timeT.text = visual.timer();
            waveT.text = wave.Now.ToString();
            ammoT.text = ammo.Remain.ToString();
            hpImage.fillAmount = playerHp.Ratio;
            fpsT.text = visual.fps().ToString();
        }
    }
}
