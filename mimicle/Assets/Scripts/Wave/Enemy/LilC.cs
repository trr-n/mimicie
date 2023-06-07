using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class LilC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        HP hp;

        float speed = 0.5f;

        Vector2 direction;

        Vector2 position;

        Vector2 firstPosition;

        float timer = 0;

        float rapid = 2;

        // bool attack = true;

        // int counter = 0;

        void Start()
        {
            hp = GetComponent<HP>();

            base.Start(hp);

            firstPosition = new(random.randfloat(3, 6), transform.position.y);

            position = transform.position;

            direction = firstPosition - position;
        }

        void Update()
        {
            Move();

            Left(gameObject);

            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);

                Score.Add(GameManager.Point.LilC);
            }
        }

        protected override void Move()
        {
            if (transform.position.x >= firstPosition.x)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }

            timer += Time.deltaTime;

            if (timer >= rapid) // && attack)
            {
                bullet.Instance(transform.position + new Vector3(-0.75f, 0), Quaternion.identity);

                // ++counter;

                timer = 0;
            }

            // if (attack && counter >= 3)
            // {
            //     attack = false;

            //     "start moving".show();
            // }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(Const.Safety))
            {
                gameObject.Remove();
            }
        }
    }
}
