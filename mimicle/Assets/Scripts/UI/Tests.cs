using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class Tests : MonoBehaviour
    {
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
        [SerializeField]
        WaveData waveData;

        void Update()
        {
            hpImage.fillAmount = playerHp.Ratio;
            fpsT.text = Render.Fps().ToString();
        }
    }
}
