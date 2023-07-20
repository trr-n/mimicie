using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class Spide : Enemy
    {
        [SerializeField]
        GameObject[] levels;

        [SerializeField]
        GameObject[] fxs;

        SpriteRenderer sr;

        /// <summary>
        /// アクティブなレベル
        /// </summary>
        int activeLevel = 0;

        /// <summary>
        /// 移動速度
        /// </summary>
        float speed = 1f;

        /// <summary>
        /// 回転速度
        /// </summary>
        int[] rotationSpeed = { 50, 90, 120 };

        /// <summary>
        /// 死亡判定フラグ
        /// </summary>
        bool dead;

        /// <summary>
        /// 透明度
        /// </summary>
        float alpha = 1f;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
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
                        fxs.Generate(transform.position);
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
