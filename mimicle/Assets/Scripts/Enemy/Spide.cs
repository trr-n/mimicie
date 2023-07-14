using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Feather.Utils;

namespace Feather
{
    public class Spide : Enemy
    {
        [SerializeField]
        GameObject[] levels;
        [SerializeField]
        GameObject[] fxs;

        int activeLevel = 0;
        float speed = 1f;
        int[] rotationSpeed = { 50, 90, 120 };
        SpriteRenderer sr;
        bool dead;
        float alpha = 1f;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            // SetLevel(0);
        }

        One once = new();
        void Update()
        {
            Move();
            if (transform.position.x <= -14)
            {
                Score.Add(Values.Point.RedSpide);
                Destroy(gameObject);
            }

            if (!(transform.GetChild(activeLevel).childCount > 0) || dead)
            {
                once.RunOnce(() =>
                {
                    Score.Add(Values.Point.Spide);
                    StartCoroutine(Fade());
                    if (sr.color.a <= 0 && speed <= 0)
                    {
                        fxs.Instance(transform.position);
                        Destroy(gameObject);
                    }
                });
            }
        }

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
            try
            {
                activeLevel = _level;
                levels[activeLevel].SetActive(true);
            }
            catch (System.IndexOutOfRangeException)
            {
                activeLevel = levels.Length - 1;
                levels[activeLevel].SetActive(true);
            }

            for (var i = 0; i < levels.Length; i++)
            {
                if (activeLevel != i)
                {
                    levels[i].SetActive(false);
                }
            }
        }

        protected override void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.Rotate(0, 0, rotationSpeed[activeLevel] * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Bullet))
            {
                dead = true;
            }
        }
    }
}
