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
        const float Span = 0.5f;
        const int X = 15;
        const float BreakTime = 2f;
        const float Offset = 3.4f;
        Stopwatch sw = new(), sp = new(true);
        List<GameObject> spawned = new List<GameObject>();
        bool isCleared1 => slain.Count >= Quota;
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
                return;
            transform.position = new(X, transform.position.y);

            // 0123 = 4
            if (sp.sf >= Span && spawnCount < Quota)
            {
                enemies[Wave.Second].Instance(new(X, lilcSpawnY), Quaternion.identity);
                sp.Restart();
                lilcSpawnY += 8 / Offset; //04255319148936f;
                spawnCount++;
            }

            if (isCleared1)
            {
                sw.Start();
                if (sw.sf >= BreakTime)
                {
                    data.ActivateWave(((int)Activate.Third));
                    slain.ResetCount();
                    sw.Destruct();
                }
            }

        }
    }
}
