using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical.Rubbish
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        List<Wave> waveList;

        [SerializeField]
        [Min(0)]
        int waveIndex = 0;
        public int Now => waveIndex;

        List<Wave> cloneList = new List<Wave>();
        int cloneIndex = 0;
        bool listEnd = false;

        void Start()
        {
            StartWave(waveIndex);
        }

        void Update()
        {
            for (int i = 0; i < cloneList.Count; i++)
                if (cloneList[i] && cloneList[i].IsDelete())
                    Destroy(cloneList[i].gameObject);
            if (!(waveList.Count <= 0 || IsEnd()) && cloneList[cloneIndex].IsEnd())
                NextWave();
        }

        void StartWave(int index) => cloneList.Add(Instantiate(waveList[index]));

        void NextWave()
        {
            if (waveIndex < waveList.Count - 1)
            {
                waveIndex++;
                cloneIndex++;
                StartWave(waveIndex);
            }
            else listEnd = true;
        }

        public bool IsEnd() => listEnd;
    }
}
