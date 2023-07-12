using System;
using System.Collections.Generic;
using UnityEngine;
using Mimicle.Extend;
using System.Collections;

namespace Mimicle
{
    public sealed class Wave1 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;
        [SerializeField]
        GameObject[] enemies;
        [SerializeField]
        Slain slain;

        List<GameObject> spawned = new List<GameObject>();
        const float BreakTime = 2f;
        const int X = 15;
        Stopwatch sw = new(), spawnTimer = new();
        int quota = 10;
        bool isDone => slain.Count >= quota;
        (int Count, float Span, float Space) Spawn = (3, 2, 1.5f);

        void OnEnable()
        {
            spawnTimer.Start();
        }

        void Update()
        {
            Make();
        }

        One MakeCharger = new();
        One Saves = new();
        void Make()
        {
            if (data.Now != 1)
            {
                return;
            }

            MakeCharger.RunOnce(() => { StartCoroutine(Chargers()); });

            foreach (var charger in spawned)
            {
                if (charger.Exist())
                {
                    return;
                }
            }

            if (isDone)
            {
                Saves.RunOnce(() =>
                {
                    var hoge = GameManager.wave[0];
                    hoge.score = Score.Now;
                    hoge.time = Score.Time;
                });
                StopCoroutine(Chargers());
                sw.Start();
                if (sw.SecondF() >= BreakTime)
                {
                    slain.ResetCount();
                    data.ActivateWave(((int)Activate.Second));
                    Stopwatch.Destruct(sw, spawnTimer);
                }
            }
        }

        IEnumerator Chargers()
        {
            float offset = 0f, spawnY = 0f;
            while (data.Now == 1 && !isDone)
            {
                yield return new WaitForSecondsRealtime(Spawn.Span);
                offset = Spawn.Space;
                var playerPos = GameObject.FindGameObjectWithTag(Constant.Player).transform.position;
                for (var i = 0; i < Spawn.Count; i++)
                {
                    spawnY = playerPos.y + offset;
                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(enemies[0].Instance(new(X, spawnY, transform.position.z), Quaternion.identity));
                    offset -= Spawn.Space;
                    yield return new WaitForSecondsRealtime(Spawn.Span / Spawn.Count);
                }
            }
        }
    }
}