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

        float spawnTimer = 0f;
        const float BreakTime = 2f;
        float breakTimer = 0f;
        const int X = 15;

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(constant.Player).transform;
        }

        void Update()
        {
            Spawn();
        }

        void Spawn()
        {
            if (data.Now != 1)
                return;
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnSpan && !IsDone())
            {
                spawnTimer = 0;
                spawned.Add(
                    enemies.Instance(
                        new(X, random.randint(-4, 4), transform.position.z), Quaternion.identity));
                print("wave1 spawn");
            }

            foreach (var i in spawned)
                if (i.IsExist())
                    return;

            if (IsDone())
            {
                breakTimer += Time.deltaTime;
                if (breakTimer >= BreakTime)
                {
                    slain.ResetCount();
                    data.ActivateWave(((int)Activate.Second));
                }
            }
        }

        bool IsDone() => slain.Count >= quota;
    }
}