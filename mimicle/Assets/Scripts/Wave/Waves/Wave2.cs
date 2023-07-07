using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using System.Collections;

namespace Mimical
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        // Transform playerTransform;
        const int Quota = 4;
        float lilcSpawnY = -3.5f;
        int spawnCount = 0;
        const int X = 15;
        const float Span = 0.5f;
        const float BreakTime = 2f;
        const float Offset = 3.4f;
        Stopwatch nextSW = new(), spanwSW = new(true);
        List<GameObject> spawned = new List<GameObject>();
        bool isDone1 => slain.Count >= Quota;
        bool isTrueClear = false;

        void OnEnable()
        {
            // playerTransform = GameObject.FindGameObjectWithTag(Constant.Player).transform;
        }

        void Update()
        {
            Make();
        }

        void Make()
        {
            if (data.Now != 2)
            {
                return;
            }
            print(GameManager.wave[0].score);

            transform.position = new(X, transform.position.y);

            // 0123 = 4
            if (spanwSW.sf >= Span && spawnCount < Quota)
            {
                enemies[Wave.Second].Instance(new(X, lilcSpawnY), Quaternion.identity);
                spanwSW.Restart();
                lilcSpawnY += 8 / Offset; //04255319148936f;
                spawnCount++;
            }

            if (isDone1)
            {
                nextSW.Start();
                if (nextSW.sf >= BreakTime)
                {
                    data.ActivateWave(((int)Activate.Third));
                    slain.ResetCount();
                    nextSW.Destruct();
                }
            }
        }
    }
}
