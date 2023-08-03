using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self
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

        List<GameObject> spawned = new();
        Stopwatch nextWaveSW = new();
        Stopwatch waveSW = new();
        Runtime makeChargers = new(), Saves = new();

        const float WaveLength = 15f;
        const float BreakTime = 2f;

        int spawnCount = 0;
        readonly (int Count, float Span, float Space) Spawn = (3, 2, 1.5f);
        readonly (int spawn, int slain) quota = (3 * 5, 3 * 3); // ノルマ

        const int X = 15;

        (Vector2 position, Runtime runner) upgrades = (default, new());

        void OnEnable()
        {
            waveSW.Start();
        }

        void Update()
        {
            if (data.Now != 1)
            {
                return;
            }

            makeChargers.RunOnce(() => { StartCoroutine(Chargers()); });

            if (!(waveSW.sf >= WaveLength && spawnCount >= quota.spawn && slain.Count >= quota.slain))
            {
                return;
            }

            // chargerが残ってたらreturn
            foreach (var charger in spawned)
            {
                if (charger)
                {
                    return;
                }
            }

            StopCoroutine(Chargers());

            upgrades.runner.RunOnce(() => upgradeItem.Generate(Vector2.zero));

            nextWaveSW.Start();
            if (nextWaveSW.SecondF() >= BreakTime)
            {
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
                yield return new WaitForSecondsRealtime(Spawn.Span);

                offset = Spawn.Space;
                Transform playerT = Gobject.GetWithTag<Transform>(Constant.Player);

                for (var i = 0; i < Spawn.Count; i++)
                {
                    spawnY = playerT.position.y + offset;

                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(chargerObj.Generate(new(X, spawnY), Quaternion.identity));

                    spawnCount++;
                    offset -= Spawn.Space;

                    yield return new WaitForSecondsRealtime(Spawn.Span / Spawn.Count);
                }
            }
        }
    }
}