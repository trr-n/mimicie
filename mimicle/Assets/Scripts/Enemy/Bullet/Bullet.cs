using UnityEngine;
using Mimicle.Extend;

namespace Mimicle
{
    public abstract class Bullet : MonoBehaviour
    {
        protected abstract void Move(float speed);
        protected abstract void TakeDamage(Collision2D info);
        protected virtual void OutOfScreen(GameObject gobject)
        {
            (float x, float y) border = (12.80f, 7.20f);
            if (gobject.transform.position.x > border.x || gobject.transform.position.x < -border.x ||
                gobject.transform.position.y > border.y || gobject.transform.position.y < -border.y)
            {
                Destroy(gameObject);
            }
        }
    }
}
