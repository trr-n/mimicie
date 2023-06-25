using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Spide : Enemy
    {
        int initLevel = 0;
        [SerializeField]
        GameObject[] levels;

        int activeLevel = 0;
        float speed = 1f;
        int[] rots = { 50, 90, 120 };

        void Start()
        {
            activeLevel = initLevel;
            SetLevel(initLevel);
        }

        void Update()
        {
            Move();
            if (transform.position.x <= -14)
                gameObject.Remove();
            if (!(transform.GetChild(activeLevel).childCount > 0))
                Destroy(gameObject);
        }

        /// <param name="_level">0-2</param>
        public void SetLevel(int _level)
        {
            activeLevel = _level;
            levels[activeLevel].SetActive(true);
            for (var i = 0; i < levels.Length; i++)
                if (activeLevel != i)
                    levels[i].SetActive(false);
        }

        protected override void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.Rotate(0, 0, rots[activeLevel] * Time.deltaTime);
        }

        protected override void OnBecameInvisible() {; }
        protected override void OnBecameVisible() {; }
    }
}
