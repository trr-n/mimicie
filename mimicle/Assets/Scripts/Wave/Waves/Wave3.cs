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

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(constant.Player).transform;
        }

        void Update()
        {
            Spawn();
            boss.ActiveLevel.print();
        }

        bool once = false;
        void Spawn()
        {
            if (data.Now != 3)
            {
                return;
            }
            print("wave3");
            transform.position = new();

            if (!once)
            {
                var b = bossObj.Instance(transform.position, Quaternion.identity);
                boss = b.GetComponent<Boss>();
                once = true;
            }
        }
    }
}
