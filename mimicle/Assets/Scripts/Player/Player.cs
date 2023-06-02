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

        GameManager manager;
        HP hp;
        Rigidbody2D rb;

        // [SerializeField]
        [Tooltip("移動速度")]
        float movingSpeed = 5;
        float rapid;

        bool shootable = true;

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
            manager = gobject.Find(Const.Manager).GetComponent<GameManager>();
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
            shootable = rapid > 0.5f;
            if (input.Pressed(manager.Fire) && !ammo.IsZero() && shootable)
            {
                gun.Shot();
                rapid = 0;
            }
        }

        void Reload()
        {
            if (input.Down(manager.Reload))
            {
                ammo.Reload();
            }
        }

        void Dead()
        {
            if (!hp.IsZero)
            {
                return;
            }
            // 仮
            scene.Load();
        }

        void Move()
        {
            transform.setpc2(-7.95f, 8.2f, -4.12f, 4.38f);
            float h = Input.GetAxis(Const.Horizontal),
                v = Input.GetAxis(Const.Vertical);
            Vector2 hv = new(h, v);
            if (manager.PlayerCtrlable)
                transform.Translate(hv * movingSpeed * Time.deltaTime);
        }

        void OnCollisionEnter(Collision info)
        {
            if (info.Compare(Const.Enemy))
            {
                hp.Damage(damage.tackle);
            }
        }
    }
}
