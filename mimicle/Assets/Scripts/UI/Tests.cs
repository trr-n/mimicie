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
        Text t;

        [SerializeField]
        Text waveT;

        [SerializeField]
        Wave wave;

        [SerializeField]
        Text ammoT;

        [SerializeField]
        Ammo ammo;

        void Update()
        {
            t.text = visual.timer(1);
            waveT.text = wave.Current.ToString();
            ammoT.text = ammo.Remain.ToString();
        }
    }
}
