using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimical
{
    public class Enemy : MonoBehaviour
    {
        protected virtual void Move(Vector2 movingDir, float speed)
        {
            transform.Translate(movingDir * speed * Time.deltaTime);
        }

        protected virtual void Leave(GameObject obj, HP hp)
        {
            if (obj.transform.position.x <= -20 || obj.transform.position.x >= 20 ||
                hp.IsZero)
            {
                Destroy(obj);
            }
        }

        // protected abstract void Attack(int dmgAmount);

        // protected void Attack();
    }
}
