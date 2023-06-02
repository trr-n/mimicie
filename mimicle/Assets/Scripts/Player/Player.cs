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

        HP hp;

        // [SerializeField]
        [Tooltip("移動速度")]
        float movingSpeed = 5;

        float rapid;

        Rigidbody2D rb;

        Vector2 preCursorPos;
        Vector2 movingDirection;
        Vector2 currentCursorPos;

        [System.Serializable]
        struct Damage
        {
            public int tackle;
            public int shooting;
        }
        [SerializeField]
        Damage damage;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            hp = GetComponent<HP>();
        }

        void Start()
        {
            Physics2D.gravity = new Vector3(0, 0, -9.81f);
            ammo.Reload();
        }

        void FixedUpdate()
        {
            Trigger();
        }

        void Update()
        {
            Move();
            Reload();
            Dead();
        }

        void Trigger()
        {
            rapid += Time.deltaTime;
            if (!ammo.IsZero() && input.Pressed(cst.Fire) && rapid > 0.5f)
            {
                gun.Shot();
                rapid = 0;
            }
        }

        void Reload()
        {
            if (input.Down(cst.Reload))
            {
                ammo.Reload();
            }
        }

        void Dead()
        {
            //* ok pass
            if (!hp.IsZero)
            {
                return;
            }
            scene.Load();
        }

        void Move()
        {
            Vector2 position = transform.position;
            position.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
            position.y = Mathf.Clamp(transform.position.y, -4.6f, 4.6f);
            transform.position = position;
            float h = Input.GetAxis(cst.Horizontal),
                v = Input.GetAxis(cst.Vertical);
            Vector2 hv = new(h, v);
            transform.Translate(hv * movingSpeed * Time.deltaTime);
        }

        public void Tackle()
        {
            hp.Damage(damage.tackle);
        }

        void OnCollisionEnter(Collision info)
        {
            if (info.Try<HP>(out var hp))
            {
                hp.Now.show();
            }
        }
    }
}
