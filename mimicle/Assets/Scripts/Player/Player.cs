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
        int tackleDamage;

        [SerializeField]
        int shootingDamage;

        [SerializeField]
        Ammo ammo;

        [SerializeField]
        Fire gun;

        // Gun
        float rapid;
        float reloading = 2;
        float reloadingTimer = 0;
        bool isReloading = false;
        public float ReloadProgress => reloadingTimer / reloading;

        GameManager manager;

        HP hp;

        Rigidbody2D rb;

        float movingSpeed = 5;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            hp = GetComponent<HP>();
            manager = gobject.Find(Const.Manager).GetComponent<GameManager>();
        }

        void Start()
        {
            ammo.Reload();
        }

        void FixedUpdate()
        {
            Trigger();
        }

        void Update()
        {
            Move();
            Dead();
            Reload();
        }

        void Trigger()
        {
            rapid += Time.deltaTime;

            var trigger = input.Pressed(manager.Fire) &&
                !ammo.IsZero() && rapid > 0.5f && !isReloading;

            if (trigger)
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
                isReloading = true;
            }

            if (isReloading)
            {
                reloadingTimer += Time.deltaTime;

                if (reloadingTimer >= reloading)
                {
                    isReloading = false;
                    reloadingTimer = 0;
                }
            }
        }

        void Dead()
        {
            if (hp.IsZero)
                scene.Load();
        }

        void Move()
        {
            transform.setpc2(-7.95f, 8.2f, -4.12f, 4.38f);

            float h = Input.GetAxis(Const.Horizontal);
            float v = Input.GetAxis(Const.Vertical);
            Vector2 moving = new(h, v);

            if (manager.PlayerCtrlable)
                transform.Translate(moving * movingSpeed * Time.deltaTime);
        }
    }
}
