using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Wave1 : WaveData
    {
        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        float spawnSpan = 1f;

        Transform playerTransform;

        float breakingTimer = 0;

        int y = 0;

        void OnEnable()
        {
            print("wave1 start method passed");
            playerTransform = GameObject.FindGameObjectWithTag(constant.Player).transform;
        }

        void Update()
        {
            Spawn();
        }

        void Spawn()
        {
            if (waves != 0)
            {
                return;
            }

            y = random.randint();
            transform.position = new(X, y);

            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= spawnSpan)
            {
                SpawnTimer = 0;

                enemies.Instance(transform.position, Quaternion.identity);
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
        }
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