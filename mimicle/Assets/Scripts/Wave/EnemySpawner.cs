using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        float span = 1;

        float timer = 0;

        public bool Spawnable { get; set; }

        void Update()
        {
            transform.position = new(15, Mathf.Sin(Time.time));
            Spawning(enemies[0]);
        }

        void Spawning(GameObject enemy)
        {
            timer += Time.deltaTime;
            if (!Spawnable && timer <= span)
                return;
            enemy.Instance(transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
