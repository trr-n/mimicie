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
        int quota = 10;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        float spawnSpan = 1f;

        [SerializeField]
        Slain slain;

        List<GameObject> spawned = new List<GameObject>();
        const float BreakTime = 2f;
        const int X = 15;
        Stopwatch sw = new(), spawnTimer = new();
        // bool isDone = false;
        bool isDone => slain.Count >= quota;
        (int count, float span, float space) spawn = (3, 2, 1.5f);

        void OnEnable()
        {
            spawnTimer.Start();
        }

        void Update()
        {
            Spawn();
        }

        float sy = 0f, ssy = 0f;
        IEnumerator Chargers()
        {
            while (data.Now == 1 && !isDone)
            {
                yield return new WaitForSecondsRealtime(spawn.span);
                sy = spawn.space;
                var playerPos = GameObject.FindGameObjectWithTag(Constant.Player).transform.position;
                for (var i = 0; i < spawn.count; i++)
                {
                    ssy = playerPos.y + sy;
                    ssy = Mathf.Clamp(ssy, -4, 4);
                    spawned.Add(enemies.Instance(new(
                            X, ssy, transform.position.z), Quaternion.identity));
                    sy -= spawn.space;
                    yield return new WaitForSecondsRealtime(spawn.span / spawn.count);
                }
            }
        }
        bool bb;
        void Spawn()
        {
            if (data.Now != 1)
                return;
            if (!bb)
            {
                StartCoroutine(Chargers());
                bb = true;
            }

            foreach (var charger in spawned)
                if (charger.Exist())
                    return;

            if (isDone)
            {
                StopCoroutine(Chargers());
                print("stop");
                sw.Start();
                // spawnTimer.Destruct();
                if (sw.SecondF() >= BreakTime)
                {
                    slain.ResetCount();
                    data.ActivateWave(((int)Activate.Second));
                    // sw.Destruct();
                    sw = spawnTimer = null;
                }
            }
        }

    }
}