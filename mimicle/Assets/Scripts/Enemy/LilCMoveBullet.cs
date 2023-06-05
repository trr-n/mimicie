using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class LilCMoveBullet : MonoBehaviour
    {
        float speed = 1;

        Vector2 direction;

        Transform playerTransform;

        void Start()
        {
            playerTransform = gobject.Find(Const.Player).transform;

            direction = playerTransform.position - transform.position;
        }

        void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Const.Player))
            {
                info.Get<HP>().Damage(Damage.LilC);

                gameObject.Remove();
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(Const.Safety))
                gameObject.Remove();
        }
    }
}