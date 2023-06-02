using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class MobSpawner : MonoBehaviour
    {
        [System.Serializable]
        struct Enemies
        {
            public GameObject obj;
        }
        [SerializeField]
        Enemies enemies;

        [SerializeField]
        Wave wave;

        void Start()
        {
            StartCoroutine(Generate());
        }

        IEnumerator Generate()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                GenerateEnemy();
            }
        }

        void GenerateEnemy()
        {
            enemies.obj.Instance(transform.position, Quaternion.identity);
        }
    }
}
