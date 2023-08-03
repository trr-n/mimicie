using System.Collections.Generic;
using UnityEngine;
using Self.Utils;
using System.Collections;

namespace Self
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        float lilcSpawnY = -3.5f;
        const float Offset = 3.4f;
        const int X = 15;

        const int Quota = 4;

        bool isDone1 => slain.Count >= Quota;

        const float BreakTime = 2f;
        Stopwatch nextSW = new();
        Stopwatch spanwSW = new(true);
        Runtime LilC = new();

        Runtime UpgradeItem = new();

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

            transform.SetPosition(X);

            LilC.RunOnce(() => StartCoroutine(MakeLilC()));

            if (isDone1)
            {
                nextSW.Start();

                if (nextSW.sf >= BreakTime)
                {
                    data.ActivateWave(((int)Activate.Third));
                    slain.ResetCount();
                    nextSW.Rubbish();
                }
            }
        }

        IEnumerator MakeLilC()
        {
            for (int count = 0; count < 4; count++)
            {
                enemies[0].Generate(new(X, lilcSpawnY));
                spanwSW.Restart();

                lilcSpawnY += 8 / Offset; //04255319148936f;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
