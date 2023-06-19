using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public enum Activate
    {
        First, Second, Third
    }

    public class WaveData : MonoBehaviour
    {
        [SerializeField]
        public Activate waves = Activate.First;

        [SerializeField]
        GameObject[] waveObjs;

        int max = 0;
        public int Max => max;

        int now4ui = 0;
        public int Now => now4ui;

        // protected const float X = 15f;

        // protected List<GameObject> spawned = new List<GameObject>();

        // [SerializeField]
        // [Tooltip("ウェーブ間の待機時間")]
        // protected int BreakTime = 2;
        // protected float BreakTimer = 0f;
        // protected float SpawnTimer = 0f;

        void Start()
        {
            max = waveObjs.Length;
            now4ui = ((int)waves);
            ActivateWave(((int)waves));
        }

        void Update()
        {
            // now = ((int)waves) + 1;
            if (input.Down(KeyCode.Return))
            {
                print("next");
                Next();
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
            // print($"index: {index}, length: {waveObjs.Length}, waves: {((int)waves)}");
            waveObjs[index].SetActive(true);
            now4ui = index + 1;

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
