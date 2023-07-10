using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cet
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField]
        Text max, now;
        [SerializeField]
        WaveData wave;

        public void Start()
        {
            max.text = wave.Max.ToString();
        }

        public void UpdateUI()
        {
            now.text = wave.Now.ToString();
        }
    }
}