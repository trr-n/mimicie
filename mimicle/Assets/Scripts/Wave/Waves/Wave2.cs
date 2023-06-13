using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject[] enemies;

        Transform playerTransform;

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
            if (data.Now != 2)
            {
                return;
            }
            print("wave2");
        }
    }
}
