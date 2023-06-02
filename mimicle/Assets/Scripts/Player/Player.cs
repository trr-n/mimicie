using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Ammo ammo;

        [SerializeField]
        Fire gun;

        // [SerializeField]
        [Tooltip("移動速度")]
        float movingSpeed = 5;

        float rapid;

        Rigidbody2D rb;

        Vector2 preCursorPos;
        Vector2 movingDirection;
        Vector2 currentCursorPos;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            Physics2D.gravity = new Vector3(0, 0, -9.81f);
            ammo.Reload();
        }

        void FixedUpdate()
        {
            rapid += Time.deltaTime;
            if (!ammo.IsZero() && input.Pressed(cst.Fire) && rapid > 0.5f)
            {
                gun.Shot();
                rapid = 0;
            }
        }

        void Update()
        {
            // reloading
            if (input.Down(cst.Reload))
            {
                ammo.Reload();
            }
            Move(movingSpeed);
        }

        void Move(float speed)
        {
            Vector2 position = transform.position;
            position.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
            position.y = Mathf.Clamp(transform.position.y, -4.6f, 4.6f);
            transform.position = position;
            float h = Input.GetAxis(cst.Horizontal),
                v = Input.GetAxis(cst.Vertical);
            Vector2 hv = new(h, v);
            transform.Translate(hv * speed * Time.deltaTime);
        }
    }
}
