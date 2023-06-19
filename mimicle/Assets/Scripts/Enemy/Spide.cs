using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Spide : Enemy
    {
        [SerializeField, Min(0)]
        int initLevel = 0;

        int level = 0;

        [SerializeField]
        GameObject[] levels;

        float speed = 1f;

        int[] rots = { 50, 90, 120 };

        void Start()
        {
            level = initLevel;
            SetLevel(level);
        }

        void Update()
        {
            Move();
            if (transform.position.x <= -14)
            {
                gameObject.Remove();
            }
        }

        void SetLevel(int _level)
        {
            level = _level;
            levels[level].SetActive(true);
            for (var i = 0; i < levels.Length; i++)
            {
                if (level != i)
                {
                    levels[i].SetActive(false);
                }
            }
        }

        protected override void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.Rotate(0, 0, rots[level] * Time.deltaTime);
        }

        protected override void OnBecameInvisible()
        {
        }

        protected override void OnBecameVisible()
        {
        }
    }
}
