using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SLilCBullet : Bullet
    {
        [SerializeField]
        float speed = 1;

        SpiralLilC slilc;
        SpriteRenderer sr;

        // Start is called before the first frame update
        void Start()
        {
            // Invoke(nameof(removal), 30);
            sr = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            Move(speed);
            OutOfScreen(gameObject);
        }

        void removal() => gameObject.Remove();

        protected override void Move(float speed)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info)
        {
            info.Get<HP>().Damage(Values.Damage.SLilC);
            Score.Add(Values.Point.RedSLilCBullet);
            gameObject.Remove();
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
                TakeDamage(info);

            if (info.Compare(constant.Bullet))
                gameObject.Remove();
        }
    }
}
