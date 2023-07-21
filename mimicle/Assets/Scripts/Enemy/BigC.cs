using System.Collections;
using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class BigC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        (float move, float rotation) speeds = (0.5f, 100);
        HP bigcHP;

        void Start()
        {
            bigcHP = GetComponent<HP>();
            bigcHP.SetMax();

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