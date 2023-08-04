using System.Collections.Generic;
using UnityEngine;
using Self.Utils;
using System.Collections;

namespace Self.Game
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        [SerializeField]
        GameObject item;

        float lilcSpawnY = -3.5f;
        const float Offset = 3.4f;
        const int X = 15;

        const int Quota = 4;

        bool isDone1 => slain.Count >= Quota;

        const float BreakTime = 2f;
        Stopwatch nextSW = new();
        Stopwatch spanwSW = new(true);
        Runner LilC = new();
        Runner drop = new();

        WaveData data;

        void OnEnable()
        {
            data = transform.parent.gameObject.GetComponent<WaveData>();
        }

        void Update()
        {
            Make();
        }

        void Make()
        {
            // if (data.ActiveWave != 2)
            if (!data.IsActiveWave(1))
            {
                return;
            }

            transform.SetPosition(X);

            LilC.Once(() => StartCoroutine(MakeLilC()));

            if (isDone1)
            {
                nextSW.Start();

                if (nextSW.sf >= BreakTime)
                {
                    drop.Once(() => item.Generate());

                    data.ActivateWave((int)Activate.Third);
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

                if (count % 2 == 0)
                {
                    Vector2 ninja = new(x: Rnd.Float(-8, 8), y: Rnd.Float(-4, 4));
                    enemies[1].Generate(ninja);
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
