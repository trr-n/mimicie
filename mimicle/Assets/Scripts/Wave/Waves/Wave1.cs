using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;

namespace Feather
{
    public sealed class Wave1 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        List<GameObject> spawned = new();
        Stopwatch nextWaveSW = new();
        Stopwatch waveSW = new();
        One MakeChargers = new();
        One Saves = new();

        const float WaveLength = 15f;
        const float BreakTime = 2f;

        int spawnCount = 0;
        readonly (int Count, float Span, float Space) Spawn = (3, 2, 1.5f);
        readonly (int spawn, int slain) quota = (3 * 5, 3 * 3); // ノルマ

        const int X = 15;

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

            MakeChargers.RunOnce(() => { StartCoroutine(Chargers()); });

            // if (WaveLength >= waveSW.sf && spawnCount <= 3 * 9 && slain.Count <= quota)
            if (!(waveSW.sf >= WaveLength && spawnCount >= quota.spawn && slain.Count >= quota.slain))
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

            Saves.RunOnce(() =>
            {
                var hoge = GameManager.wave[0];
                hoge.score = Score.Now;
                hoge.time = Score.Time;
                StopCoroutine(Chargers());
            });

            nextWaveSW.Start();
            if (nextWaveSW.SecondF() >= BreakTime)
            {
                slain.ResetCount();
                data.ActivateWave(((int)Activate.Second));
                Stopwatch.Rubbish(nextWaveSW);
            }
        }

        IEnumerator Chargers()
        {
            spawnCount = 0;
            float offset = 0f, spawnY = 0f;
            // while (data.Now == 1 && !isDone)
            while (true)
            {
                yield return new WaitForSecondsRealtime(Spawn.Span);
                offset = Spawn.Space;
                var playerPos = GameObject.FindGameObjectWithTag(Constant.Player).transform.position;
                for (var i = 0; i < Spawn.Count; i++)
                {
                    spawnCount++;
                    spawnY = playerPos.y + offset;
                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(enemies[0].Instance(new(X, spawnY), Quaternion.identity));
                    offset -= Spawn.Space;
                    yield return new WaitForSecondsRealtime(Spawn.Span / Spawn.Count);
                }
            }
        }
    }
}