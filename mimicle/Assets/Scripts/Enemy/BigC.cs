using System.Collections;
using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class BigC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        /// <summary>
        /// 速度
        /// </summary>
        (float move, float rotation) speeds = (0.5f, 100);

        /// <summary>
        /// 体力
        /// </summary>
        HP bigcHP;

        void Start()
        {
            bigcHP = GetComponent<HP>();
            bigcHP.Reset();

            StartCoroutine(Attack());
        }

        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            Move();

            if (bigcHP.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
            }
        }

        protected override void Move()
        {
            transform.Translate(Time.deltaTime * speeds.move * Vector2.left, Space.World);
            transform.Rotate(0, 0, speeds.rotation * Time.deltaTime);
        }

        IEnumerator Attack()
        {
            while (Time.timeScale != 0)
            {
                yield return new WaitForSeconds(speeds.rotation / 360);

                bullet.Generate(transform.position, transform.rotation);
            }
        }
    }
}