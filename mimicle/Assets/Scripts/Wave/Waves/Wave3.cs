using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Wave3 : WaveData
    {
        void OnEnable()
        {
            print("wave3 start method passed");
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
            SpawnTimer += Time.deltaTime;
            print("wave3: " + SpawnTimer);
        }
    }
}
