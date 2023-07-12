using System.Collections.Generic;
using UnityEngine;
using Mimicle.Extend;
using System.Collections;

namespace Mimicle
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

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
        One LilC = new();

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
            transform.SetPosition(x: X);

            LilC.RunOnce(() => StartCoroutine(MakeLilC()));

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

        IEnumerator MakeLilC()
        {
            for (int i = 0; i < 4; i++)
            {
                enemies[0].Instance(new(X, lilcSpawnY));
                spanwSW.Restart();
                lilcSpawnY += 8 / Offset; //04255319148936f;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
