using System.Collections;
using UnityEngine;
using Self.Utility;

namespace Self
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
            Move();

            if (bigcHP.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
            }
        }

        protected override void Move()
        {
            transform.Translate(Vector2.left * speeds.move * Time.deltaTime, Space.World);
            transform.Rotate(0, 0, speeds.rotation * Time.deltaTime);
        }

        IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(speeds.rotation / 360);
                bullet.Generate(transform.position, transform.rotation);
            }
        }
    }
}