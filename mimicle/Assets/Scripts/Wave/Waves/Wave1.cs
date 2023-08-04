using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public sealed class Wave1 : MonoBehaviour
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject chargerObj;

        [SerializeField]
        Slain slain;

        [SerializeField]
        GameObject upgradeItem;

        readonly List<GameObject> spawned = new();
        readonly Stopwatch nextWaveSW = new();
        readonly Stopwatch waveSW = new();
        readonly Runner makeChargers = new();

        const float WaveLength = 15f;
        const float BreakTime = 2f;

        int spawnCount = 0;
        (int Count, float Span, float Space) Spawn => (3, 2, 1.5f);
        (int Spawn, int Slain) Quota => (Spawn.Count * 5, Spawn.Count * 3); // ノルマ

        const int X = 15;

        readonly Runner upg = new();

        void OnEnable()
        {
            waveSW.Start();
        }

        void Update()
        {
            // if (data.ActiveWave != 1)
            if (!data.IsActiveWave(0))
            {
                return;
            }

            makeChargers.Once(() => { StartCoroutine(Chargers()); });

            if (!(waveSW.sf >= WaveLength && spawnCount >= Quota.Spawn && slain.Count >= Quota.Slain))
            {
                return;
            }

            foreach (var charger in spawned)
            {
                if (charger)
                {
                    return;
                }
            }

            StopCoroutine(Chargers());

            nextWaveSW.Start();
            if (nextWaveSW.SecondF() >= BreakTime)
            {
                upg.Once(() => upgradeItem.Generate(Vector2.zero));

                slain.ResetCount();
                data.ActivateWave((int)Activate.Second);
                Stopwatch.Rubbish(nextWaveSW);
            }
        }

        IEnumerator Chargers()
        {
            spawnCount = 0;
            float offset = 0f, spawnY = 0f;

            while (true)
            {
                yield return new WaitForSeconds(Spawn.Span);

                offset = Spawn.Space;
                Transform playerT = Gobject.GetWithTag<Transform>(Constant.Player);

                for (var count = 0; count < Spawn.Count; count++)
                {
                    spawnY = playerT.position.y + offset;

                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(chargerObj.Generate(new(X, spawnY), Quaternion.identity));

                    spawnCount++;
                    offset -= Spawn.Space;

                    yield return new WaitForSeconds(Spawn.Span / Spawn.Count);
                }
            }
        }
    }
}