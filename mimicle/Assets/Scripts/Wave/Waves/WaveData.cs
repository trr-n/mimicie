using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public enum Activate { First, Second, Third }

    public class WaveData : MonoBehaviour
    {
        [SerializeField]
        public Activate waves = Activate.First;

        [SerializeField]
        GameObject[] waveObjs;

        [SerializeField]
        WaveUI waveUI;

        int max = 0;
        public int Max => max;

        int now4ui = 0;

        public int Now4UI => now4ui;
        public int ActiveWave => now4ui - 1;

        public bool IsDone { get; set; }

        public bool IsActiveWave(int wave) => wave == ActiveWave;

        void Start()
        {
            max = waveObjs.Length;
            now4ui = (int)waves;

            ActivateWave((int)waves);
            waveUI.Start();
        }

        public void Next()
        {
            now4ui = ((int)waves) + 1;
            waveObjs[now4ui - 1].SetActive(true);

            for (int index = 0; index < waveObjs.Length; index++)
            {
                if (now4ui - 1 != index)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }

        public void ActivateWave(int n)
        {
            waveObjs[n].SetActive(true);
            now4ui = n + 1;
            waveUI.UpdateUI();

            for (var index = 0; index < waveObjs.Length; index++)
            {
                if (index != n)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }
    }
}
