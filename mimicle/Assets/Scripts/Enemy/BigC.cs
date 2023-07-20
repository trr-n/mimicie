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
        HP hp;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            StartCoroutine(Attack());
        }

        void Update()
        {
            Move();
            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
            }
        }

        protected override void Move() => transform.Translate(Vector2.left * speeds.move * Time.deltaTime, Space.World);

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