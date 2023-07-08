using System.Collections;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SpiralLilC : Enemy
    {
        [SerializeField]
        GameObject bullet;

        (float move, float rotation) speeds = (0.5f, 100);
        HP hp;
        Rigidbody2D rb;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
            rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            StartCoroutine(Attack());
        }

        void _FixedUpdate()
        {
            Move();
        }

        void Update()
        {
            Rotate();
            if (hp.IsZero)
            {
                AddSlainCountAndRemove(gameObject);
            }
        }

        IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(speeds.rotation / 360);
                bullet.Instance(transform.position, transform.rotation);
            }
        }

        void Rotate() => transform.Rotate(0, 0, speeds.rotation * Time.deltaTime);

        protected override void Move() => transform.position += (Vector3)Vector2.left * speeds.move * Time.deltaTime;
    }
}