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
        int[] rotationSpeed = { 50, 90, 120 };
        SpriteRenderer sr;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            activeLevel = initLevel;
            SetLevel(initLevel);
        }

        void Update()
        {
            Move();
            if (transform.position.x <= -14)
                gameObject.Remove();

            if (!(transform.GetChild(activeLevel).childCount > 0))
            {
                if (bb)
                {
                    bb = false;
                    Score.Add(Values.Point.Spide);
                    StartCoroutine(Fade());
                    if (sr.color.a <= 0 && speed <= 0) //TODO fx
                        Destroy(gameObject);
                }
            }
        }
        bool bb = true;
        float alpha = 1f;
        IEnumerator Fade()
        {
            while (sr.color.a > 0)
            {
                yield return null;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
                alpha -= 0.02f;
                speed -= 0.02f;
            }
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
            transform.Rotate(0, 0, rotationSpeed[activeLevel] * Time.deltaTime);
        }

        protected override void OnBecameInvisible() {; }
        protected override void OnBecameVisible() {; }
    }
}
