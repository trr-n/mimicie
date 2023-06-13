using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SpinalCharger : Enemy
    {
        HP hp;

        float rotating = 10;

        void Start()
        {
            hp = GetComponent<HP>();
            base.Start(hp);
        }

        void Update()
        {
            Move();
        }

        protected override void Move()
        {
            // transform.Rotate(Quaternion.Euler(0, 0, rotating * Time.deltaTime));
        }

        protected override void OnBecameVisible() {; }
        protected override void OnBecameInvisible() {; }
    }
}