using UnityEngine;
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
        Image hpImage;

        [SerializeField]
        HP playerHp;

        [SerializeField]
        Text fpsT;

        [SerializeField]
        Player player;

        [SerializeField]
        Text scoreT;

        // [SerializeField]
        // Score score;

        [SerializeField]
        WaveData waveData;

        [SerializeField]
        Text waveT;

        void Update()
        {
            hpImage.fillAmount = playerHp.Ratio;
            timeT.text = Score.Time().ToString();
            nowWaveT.text = wave.Now.ToString();
            maxWaveT.text = wave.Max.ToString();
            fpsT.text = Visual.Fps().ToString();
            scoreT.text = Score.Now.ToString();
            waveT.text = waveData.Now.ToString();
        }
    }
}
