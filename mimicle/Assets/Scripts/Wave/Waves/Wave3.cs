using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Wave3 : MonoBehaviour
    {
        [SerializeField]
        WaveData data;

        void OnEnable()
        {
        }

        void Update()
        {
            Spawn();
        }

        void Spawn()
        {
            if (data.Now != 3)
            {
                return;
            }
            print("wave3");
        }
    }
}
