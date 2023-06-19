using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Wave3 : MonoBehaviour
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject bossObj;

        Boss boss;

        Transform playerTransform;

        void Start()
        {
            bossObj.SetActive(false);
        }

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
            if (data.Now != 3)
            {
                return;
            }
            print("wave3");
            transform.position = new();
            bossObj.SetActive(true);
        }
    }
}
