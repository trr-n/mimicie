using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Wave2 : WaveData
    {
        void OnEnable()
        {
            print("wave2 start method passed");
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
            print("wave2: " + SpawnTimer);
        }
    }
}
