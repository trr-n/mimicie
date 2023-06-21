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
        // [SerializeField]
        // GameObject bossObj;
        [SerializeField, Tooltip("0:charger\n1:lilc\n2:slilc\n3:spide")]
        GameObject[] mobs;
        // [SerializeField]
        // GameObject uis;
        [SerializeField]
        GameObject[] bossRelated;

        Transform playerTransform;

        void Start()
        {
            foreach (var i in bossRelated)
                i.SetActive(false);
            // bossObj.SetActive(false);
            // uis.SetActive(false);
        }

        void OnEnable()
        {
            playerTransform = GameObject.FindGameObjectWithTag(constant.Player).transform;
        }

        void Update()
        {
            Spawn();
        }

        bool once = true;
        void Spawn()
        {
            if (data.Now != 3)
                return;
            print("wave3");
            transform.position = new();
            if (once)
            {
                foreach (var i in bossRelated)
                    i.SetActive(true);
                // bossObj.SetActive(true);
                // uis.SetActive(true);
                once = false;
            }
        }
    }
}
