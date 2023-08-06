using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public enum Activate { First, Second, Third }

    public class WaveData : MonoBehaviour
    {
        public Activate waves = Activate.First;

        [SerializeField]
        GameObject[] waveObjs;

        [SerializeField]
        WaveUI waveUI;

        int max = 0;
        public int Max => max;

        int current4ui = 0;

        public int Current4UI => current4ui;
        public int CurrentActive => current4ui - 1;

        public bool IsDone { get; set; }

        public bool IsActiveWave(int wave) => wave == CurrentActive;

        void Start()
        {
            max = waveObjs.Length;
            current4ui = (int)waves;

            ActivateWave((int)waves);
            waveUI.Start();
        }

        public void Next()
        {
            current4ui = ((int)waves) + 1;
            waveObjs[current4ui - 1].SetActive(true);

            for (ushort index = 0; index < waveObjs.Length; index++)
            {
                if (current4ui - 1 != index)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }

        public void ActivateWave(int n)
        {
            waveObjs[n].SetActive(true);
            current4ui = n + 1;
            waveUI.UpdateUI();

            for (ushort index = 0; index < waveObjs.Length; index++)
            {
                if (index != n)
                {
                    waveObjs[index].SetActive(false);
                }
            }
        }
    }
}
