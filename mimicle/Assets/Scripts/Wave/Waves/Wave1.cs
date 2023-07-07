using System;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using System.Collections;

namespace Mimical
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
        (int Count, float Span, float Space) spawn = (3, 2, 1.5f);

        void OnEnable()
        {
            spawnTimer.Start();
        }

        void Update()
        {
            Spawn();
        }

        One one = new();
        void Spawn()
        {
            if (data.Now != 1)
            {
                return;
            }

            one.Once(() => { StartCoroutine(Chargers()); });

            foreach (var charger in spawned)
            {
                if (charger.Exist())
                {
                    return;
                }
            }

            if (isDone)
            {
                one1k.Once(() =>
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
        One one1k = new();

        IEnumerator Chargers()
        {
            float offset = 0f, spawnY = 0f;
            while (data.Now == 1 && !isDone)
            {
                yield return new WaitForSecondsRealtime(spawn.Span);
                offset = spawn.Space;
                var playerPos = GameObject.FindGameObjectWithTag(Constant.Player).transform.position;
                for (var i = 0; i < spawn.Count; i++)
                {
                    spawnY = playerPos.y + offset;
                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(enemies.Instance(new(X, spawnY, transform.position.z), Quaternion.identity));
                    offset -= spawn.Space;
                    yield return new WaitForSecondsRealtime(spawn.Span / spawn.Count);
                }
            }
        }
    }
}