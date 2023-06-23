using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

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

        Transform playerTransform;
        List<GameObject> spawned = new List<GameObject>();

        const float BreakTime = 2f;
        const int X = 15;
        Stopwatch sw, spawnTimer;

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(Constant.Player).transform;
            sw = new();
            spawnTimer = new();
            spawnTimer.Start();
        }

        void Update()
        {
            Spawn();
            print($"NestWave: {sw.SecondF()}, SpawnTimer: {spawnTimer.SecondF()}");
        }

        void Spawn()
        {
            if (data.Now != 1)
                return;
            if (spawnTimer.SecondF() >= spawnSpan && !IsDone())
            {
                spawnTimer.Restart();
                spawned.Add(enemies.Instance(
                    new(X, AtRandom.randint(-4, 4), transform.position.z), Quaternion.identity));
            }

            foreach (var i in spawned)
                if (i.IsExist())
                    return;

            if (IsDone())
            {
                sw.Start();
                spawnTimer.Stop();
                if (sw.SecondF() >= BreakTime)
                {
                    slain.ResetCount();
                    data.ActivateWave(((int)Activate.Second));
                    sw.Stop();
                }
            }
        }

        bool IsDone() => slain.Count >= quota;
    }
}