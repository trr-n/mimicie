using UnityEngine;
using Self.Utility;

namespace Self
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
        public int Now => now4ui;
        public bool IsDone { get; set; }

        void Start()
        {
            max = waveObjs.Length;
            now4ui = ((int)waves);
            ActivateWave(((int)waves));
            waveUI.Start();
        }

        void Update()
        {
            if (IsDone)
            {
                ;
            }
        }

        public void Next()
        {
            now4ui = ((int)waves) + 1;
            waveObjs[now4ui - 1].SetActive(true);

            for (int i = 0; i < waveObjs.Length; i++)
            {
                if (now4ui - 1 != i)
                {
                    waveObjs[i].SetActive(false);
                }
            }
        }

        public void ActivateWave(int index)
        {
            waveObjs[index].SetActive(true);
            now4ui = index + 1;
            waveUI.UpdateUI();

            for (var i = 0; i < waveObjs.Length; i++)
            {
                if (i != index)
                {
                    waveObjs[i].SetActive(false);
                }
            }
        }
    }
}
