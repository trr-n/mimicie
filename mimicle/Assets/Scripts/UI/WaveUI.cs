using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField]
        Text max, now;
        [SerializeField]
        WaveData wave;

        public void Start()
        {
            print(wave.Max);
            max.text = wave.Max.ToString();
        }

        void Update()
        {
        }

        public void UpdateUI()
        {
            now.text = wave.Now.ToString();
        }
    }
}