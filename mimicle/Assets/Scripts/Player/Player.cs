using UnityEngine;
using Mimical.Extend;
using UnityEngine.UI;

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

        [SerializeField]
        GameManager manager;

        [SerializeField]
        Text reloadingT;

        // Gun
        float rapid;
        float timeToReload = 0f;
        public float Time2Reload => timeToReload;
        float rtime = 2f;
        float reloadingTimer = 0;
        bool isReloading = false;
        public bool IsReloading => isReloading;
        public float ReloadProgress => reloadingTimer / timeToReload;
        float movingSpeed = 5;
        HP hp;

        RaycastHit2D hit;
        public RaycastHit2D Hit => hit;

        void Awake()
        {
            hp = GetComponent<HP>();
            manager ??= Gobject.Find(Constant.Manager).GetComponent<GameManager>();
        }

        void Start() => ammo.Reload();

        void FixedUpdate()
        {
            Trigger();
        }

        void Update()
        {
            Move();
            Dead();
            Reload();
            DrawRaid();
        }

        void DrawRaid()
        {
            var r = new Ray(transform.position, Vector2.right);
            // Debug.DrawRay(ray.origin, ray.direction);
            hit = Physics2D.Raycast(r.origin, r.direction, 20.48f, 1 << 9);
            if (!hit.collider)
                return;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
#endif

        void Trigger()
        {
            rapid += Time.deltaTime;
            if (!(SelfInput.Pressed(Values.Key.Fire) && !ammo.IsZero() && rapid > 0.5f && !isReloading))
                return;
            gun.Shot();
            rapid = 0;
        }

        void Reload()
        {
            //* Fix: リロード中値が変わらないように
            if (!isReloading)
                timeToReload = (1 - ammo.Ratio) * rtime; // リロード時間=残弾数の割合*2秒
            reloadingT.text = $"time: {timeToReload.newline()}timer: {reloadingTimer}";
            if (SelfInput.Down(Values.Key.Reload))
            {
                ammo.Reload();
                isReloading = true;
            }

            if (isReloading)
            {
                reloadingTimer += Time.deltaTime;
                // print(reloadingTimer);
                if (reloadingTimer >= timeToReload)
                {
                    isReloading = false;
                    reloadingTimer = 0;
                }
            }
        }

        // TODO
        void Dead()
        {
            if (hp.IsZero)
            {
                Section.Load();
            }
        }

        void Move()
        {
            transform.setpc2(-7.95f, 8.2f, -4.12f, 4.38f);
            float h = Input.GetAxis(Constant.Horizontal),
                v = Input.GetAxis(Constant.Vertical);
            Vector2 moving = new(h, v);
            if (!manager.PlayerCtrlable)
                return;
            transform.Translate(moving * movingSpeed * Time.deltaTime);
        }
    }
}
