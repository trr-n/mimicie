using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using DG.Tweening;

namespace Mimical
{
    public sealed class LilC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        HP hp;
        BoxCollider2D col;

        Vector2 position;
        Vector2 firstPosition;
        Vector2 direction;

        float timer = 0;
        float rapid = 2;

        float speed = 0.5f;

        // bool attack = true;

        // int counter = 0;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);

            col = GetComponent<BoxCollider2D>();

            firstPosition = new(random.randint(3, 6), transform.position.y);
            position = transform.position;
            direction = firstPosition - position;
            transform.DOMove(firstPosition, 10).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Move();
            Left(gameObject);

            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
                Score.Add(Values.Point.LilC);
            }
        }

        protected override void Move()
        {
            if (transform.position.x >= firstPosition.x)
            {
                // transform.Translate(direction * speed * Time.deltaTime);
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

        protected override void OnBecameInvisible()
        {
            col.isTrigger = true;
        }

        protected override void OnBecameVisible()
        {
            col.isTrigger = false;
        }

        // void OnCollisionExit2D(Collision2D info)
        // {
        //     if (info.Compare(constant.Safety))
        //     {
        //         gameObject.Remove();
        //     }
        // }
    }
}
