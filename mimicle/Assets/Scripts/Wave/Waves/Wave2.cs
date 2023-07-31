using System.Collections.Generic;
using UnityEngine;
using Self.Utility;
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

        [SerializeField]
        GameObject player, sidegunUpgradeItem;

        const int Quota = 4;
        float lilcSpawnY = -3.5f;
        const int X = 15;
        const float BreakTime = 2f;
        const float Offset = 3.4f;
        Stopwatch nextSW = new(), spanwSW = new(true);
        bool isDone1 => slain.Count >= Quota;
        Special LilC = new(), UpgradeItem = new();

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

            UpgradeItem.Runner(() =>
            {
                Vector3 playerPos = player.transform.position;
                sidegunUpgradeItem.Generate();
            });

            transform.SetPosition(X);

            LilC.Runner(() => StartCoroutine(MakeLilC()));

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
