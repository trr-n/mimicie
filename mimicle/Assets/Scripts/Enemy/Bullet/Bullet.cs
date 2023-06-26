using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public abstract class Bullet : MonoBehaviour
    {
        protected abstract void Move(float speed);
        protected abstract void TakeDamage(Collision2D info);
        protected void OutOfScreen(GameObject g)
        {
            (float x, float y) borders = (12.80f, 7.20f);
            if (g.transform.position.x > borders.x || g.transform.position.x < -borders.x ||
                g.transform.position.y > borders.y || g.transform.position.y < -borders.y)
                g.Remove();
        }
    }
}
