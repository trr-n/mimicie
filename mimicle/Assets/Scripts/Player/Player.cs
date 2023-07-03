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
        [SerializeField]
        GameManager manager;
        [SerializeField]
        AudioClip[] damageSE;
        [SerializeField]
        Parry parry;

        // Gun
        float rapid;
        Stopwatch rapidSW = new(true);
        float rapidSpan = 0.5f;
        float time2reload = 0f;
        public float Time2Reload => time2reload;
        bool isReloading = false;
        public bool IsReloading => isReloading;
        public float ReloadProgress; float movingSpeed = 5;
        HP hp;
        RaycastHit2D hit;
        public RaycastHit2D Hit => hit;
        Stopwatch reloadsw;
        public float Reload__ => reloadsw.SecondF(2);
        public float maxs => 1.5f;
        SpriteRenderer sr;
        Stopwatch sw = new();
        new BoxCollider2D collider;
        public bool NotNinnin = false;

        void Awake()
        {
            hp = GetComponent<HP>();
            manager ??= Gobject.Find(Constant.Manager).GetComponent<GameManager>();
            reloadsw = new();
            sr = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
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
            collider.isTrigger = parry.IsParry;
            if (sw.sf >= 0.2f)
                sr.color = Color.white;
        }

        void DrawRaid()
        {
            var r = new Ray(transform.position, Vector2.right);
            hit = Physics2D.Raycast(r.origin, r.direction, 20.48f, 1 << 9 | 1 << 10);
        }

        void Trigger()
        {
            if (NotNinnin = !(Mynput.Pressed(Values.Key.Fire) && !ammo.IsZero() && rapidSW.sf > rapidSpan && !isReloading))
                return;
            gun.Shot();
            rapidSW.Restart();
        }

        void Reload()
        {
            ReloadProgress = reloadsw.sf / time2reload;
            if (!isReloading)
                // リロード時間=残弾数の割合*n秒
                time2reload = (1 - ammo.Ratio) * maxs;
            if (Mynput.Down(Values.Key.Reload))
            {
                // StartCoroutine(ammoUI.reload(time2reload));
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

        // when a player is dead
        void Dead()
        {
            if (hp.IsZero)
            {
                manager.PlayerIsDead();
            }
        }

        void Move()
        {
            if (!manager.Ctrlable)
                return;
            transform.setpc2(-7.95f, 8.2f, -4.12f, 4.38f);
            (float h, float v) axis = (Input.GetAxis(Constant.Horizontal), Input.GetAxis(Constant.Vertical));
            transform.Translate(new Vector2(axis.h, axis.v) * movingSpeed * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D _)
        {
            if (!parry.IsParry)
            {
                sw.Restart();
                sr.color = Color.red;
            }
        }
    }
}
