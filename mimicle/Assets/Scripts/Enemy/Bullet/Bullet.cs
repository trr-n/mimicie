using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public abstract class Bullet : MonoBehaviour
    {
        // [SerializeField]
        // GameObject bullet;

        protected abstract void Move(float speed);

        protected abstract void Attack(Collision2D info);

        protected abstract void Attack();
    }
}
