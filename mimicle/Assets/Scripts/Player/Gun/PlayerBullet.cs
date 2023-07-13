using UnityEngine;

namespace UnionEngine.Extend
{
    public class PlayerBullet : Bullet
    {
        Vector2 direction;

        void Start()
        {
            direction = transform.right;
        }

        void Update()
        {
            OutOfScreen(gameObject);
            Move(20);
        }

        protected override void Move(float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        protected override void TakeDamage(Collision2D info) {; }
    }
}