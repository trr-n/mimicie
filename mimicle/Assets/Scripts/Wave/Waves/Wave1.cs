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
        int y = 0;

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
            {
                return;
            }

            y = random.randint();
            transform.position = new(X, y);

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnSpan && !IsDone())
            {
                spawnTimer = 0;
                spawned.Add(enemies.Instance(transform.position, Quaternion.identity));
                print("wave1 spawn");
            }

            foreach (var i in spawned)
            {
                if (i.IsExist())
                {
                    print("yet next wave");
                    return;
                }
            }

            if (IsDone())
            {
                breakTimer += Time.deltaTime;
                if (breakTimer >= BreakTime)
                {
                    print("clear this wave");
                    data.ActivateWave(((int)Activate.Second));
                }
            }
        }

        bool IsDone() => slain.Count >= quota;
    }
}

/* 残骸
        public void First()
        {
            if (!inProgress(Wave.First))
            {
                return;
            }

            quota.timer += Time.deltaTime;

            "Wave1".show();

            transform.position = new(X, playerTransform.position.y);

            wave.Set(Wave.First);

            if (quota.timer >= quota.span &&
                slain.Count <= quota.quota)
            {
                spawnedEnemies.Add(
                    enemy.Instance(transform.position, Quaternion.identity));

                quota.timer = 0;
            }

            foreach (var i in spawnedEnemies)
            {
                if (i.IsExist())
                {
                    return;
                }
            }

            if (slain.Count >= quota.quota)
            {
                breakingTimer += Time.deltaTime;

                if (breakingTimer >= BreakingTime)
                {
                    "active 2".show();

                    ActivateWave(Wave.Second);
                    slain.ResetCount();

                    breakingTimer = 0;
                }
            }
        }
    }
}
*/