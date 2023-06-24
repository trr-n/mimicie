using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        Transform playerTransform;

        int quota = 4;
        float lilcSpawnY = -3.5f;
        int spawnCount = 0;
        float timer = 0f;
        float span = 0.5f;
        const int X = 15;
        const float BreakTime = 2f;
        float breakTimer = 0f;

        List<GameObject> spawned = new List<GameObject>();

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(Constant.Player).transform;
        }

        void Update()
        {
            Spawn();
        }

        void Spawn()
        {
            if (data.Now != 2)
                return;
            print("wave2");
            transform.position = new(X, transform.position.y);
            timer += Time.deltaTime;

            // 0123 = 4
            if (timer >= span && spawnCount < quota)
            {
                enemies[Wave.Second].Instance(new(X, lilcSpawnY), Quaternion.identity);
                timer = 0;
                spawnCount++;
                lilcSpawnY += 8 / 3.4f; //04255319148936f;
            }

            if (IsDone())
            {
                breakTimer += Time.deltaTime;
                if (breakTimer >= BreakTime)
                {
                    data.ActivateWave(((int)Activate.Third));
                    slain.ResetCount();
                    breakTimer = 0;
                }
            }

            bool IsDone() => slain.Count >= quota;
        }
    }
}
