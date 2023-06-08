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
            playerTransform = gobject.Find(constant.Player).transform;

            direction = playerTransform.position - transform.position;
        }

        void Update() => transform.Translate(direction * speed * Time.deltaTime);

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Player))
            {
                info.Get<HP>().Damage(GameManager.Dmg.LilC);

                Score.Add(GameManager.Point.RedLilCBullet);

                gameObject.Remove();
            }

            if (info.Compare(constant.Bullet))
            {
                gameObject.Remove();
            }
        }

        void OnCollisionExit2D(Collision2D info)
        {
            if (info.Compare(constant.Safety))
            {
                gameObject.Remove();
            }
        }
    }
}