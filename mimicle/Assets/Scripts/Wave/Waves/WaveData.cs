using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class WaveData : MonoBehaviour
    {
        public enum Waves
        {
            First, Second, Third
        }

        [SerializeField]
        protected Waves waves = Waves.First;

        [SerializeField]
        GameObject[] waveObjs;

        protected List<GameObject> spawned = new List<GameObject>();

        protected int max = 0;
        public int Max => max;

        protected int now = 0;
        public int Now => now;

        protected const float X = 15f;

        protected const int BreakTime = 2;
        protected float BreakTimer = 0f;
        protected float SpawnTimer = 0f;

        void Start()
        {
            max = waveObjs.Length;
        }

        void Update()
        {
            ActivateWave(((int)waves));
            now = ((int)waves) + 1;
        }

        protected void ActivateWave(int index)
        {
            waveObjs[index].SetActive(true);

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
