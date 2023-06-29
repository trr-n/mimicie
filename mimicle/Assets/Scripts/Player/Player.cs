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
        [SerializeField]
        AudioClip[] damageSE;

        // Gun
        float rapid;
        Stopwatch rapidSW = new(true);
        float rapidSpan = 0.5f;
        float time2reload = 0f;
        public float Time2Reload => time2reload;
        float rtime = 2f;
        bool isReloading = false;
        public bool IsReloading => isReloading;
        public float ReloadProgress; float movingSpeed = 5;
        HP hp;
        RaycastHit2D hit;
        public RaycastHit2D Hit => hit;
        Stopwatch reloadsw;

        void Awake()
        {
            hp = GetComponent<HP>();
            manager ??= Gobject.Find(Constant.Manager).GetComponent<GameManager>();
            reloadsw = new();
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
            hit = Physics2D.Raycast(r.origin, r.direction, 20.48f, 1 << 9 | 1 << 10);
            if (!hit.collider)
                return;
        }

        void Trigger()
        {
            if (!(Mynput.Pressed(Values.Key.Fire) && !ammo.IsZero() && rapidSW.sf > rapidSpan && !isReloading))
                return;
            gun.Shot();
            rapidSW.Restart();
        }

        void Reload()
        {
            ReloadProgress = reloadsw.sf / time2reload;
            if (!isReloading)
                // リロード時間=残弾数の割合*2秒
                time2reload = (1 - ammo.Ratio) * rtime; //! fix: 値が0以下になる
            reloadingT.text = $"time: {time2reload.newline()}timer: {reloadsw.SecondF()}";
            if (Mynput.Down(Values.Key.Reload))
            {
                ammo.Reload();
                isReloading = true;
            }

            if (isReloading)
            {
                reloadsw.Start();
                if (!(reloadsw.SecondF() >= time2reload))
                    return;
                isReloading = false;
                reloadsw.Reset();
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
            if (!manager.PlayerCtrlable)
                return;
            transform.Translate(new Vector2(
                Input.GetAxis(Constant.Horizontal), Input.GetAxis(Constant.Vertical)) * movingSpeed * Time.deltaTime);
        }
    }
}
