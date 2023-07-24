using UnityEngine;
using Self.Utils;

namespace Self
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

        [SerializeField]
        CircleUI circleUI;

        [SerializeField]
        SideGun sidegun;

        /// <summary>
        /// 連射速度
        /// </summary>
        const float RapidSpan = 0.1f;

        float time2reload = 0f;
        /// <summary>
        /// リロードにかかる時間
        /// </summary>
        public float Time2Reload => time2reload;

        bool isReloading = false;
        /// <summary>
        /// リロード中ならTrue
        /// </summary>
        public bool IsReloading => isReloading;

        /// <summary>
        /// リロードの進捗
        /// </summary>
        public float ReloadProgress;

        /// <summary>
        /// 移動速度
        /// </summary> 
        float movingSpeed = 5;

        /// <summary>
        /// プレイヤーのHP
        /// </summary>
        HP playerHP;

        /// <summary>
        /// リロード用ストップウォッチ
        /// </summary>
        Stopwatch reloadSW = new();

        /// <summary>
        /// 連射用ストップウォッチ
        /// </summary>
        Stopwatch rapidSW = new(true);

        /// <summary>
        /// 被弾のやつ用ストップウォッチ
        /// </summary>
        Stopwatch sw = new();

        /// <summary>
        /// プレイヤーのSR
        /// </summary>
        SpriteRenderer playerSR;

        /// <summary>
        /// プレイヤーの当たり判定
        /// </summary>
        Collider2D playerCol;

        /// <summary>
        /// 忍者がにんにんしてたらFalse
        /// </summary>
        public bool NotNinnin = false;

        RaycastHit2D hit;
        /// <summary>
        /// レーザーの着弾地点表示用
        /// </summary>
        public RaycastHit2D Hit => hit;

        /// <summary>
        /// リロード用ストップウォッチ
        /// </summary>
        public float Reload__ => reloadSW.SecondF(2);

        /// <summary>
        /// 最大リロード時間
        /// </summary>
        const float MaxReloadTime = 1f;

        /// <summary>
        /// 残弾数の割合
        /// </summary>
        public float PreReloadRatio = 0f;

        /// <summary>
        /// セーブデータ書き込み用
        /// </summary>
        One a = new();

        /// <summary>
        /// 上下の銃の表示数
        /// </summary>
        int upgradeCount = 0;

        void Awake()
        {
            manager ??= Gobject.Find(Constant.Manager).GetComponent<GameManager>();

            playerHP = GetComponent<HP>();
            playerSR = GetComponent<SpriteRenderer>();
            playerCol = GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            ammo.Reload();
        }

        void Update()
        {
            Move();
            Dead();
            Reload();

            hit = Physics2D.Raycast(transform.position, Vector2.right, 20.48f, 1 << 9 | 1 << 10);
            playerCol.isTrigger = parry.IsParry;

            if (sw.sf >= 0.2f)
            {
                playerSR.color = Color.white;
            }
        }

        void FixedUpdate()
        {
            if (NotNinnin = !(Feed.Pressed(Values.Key.Fire) && !ammo.IsZero() && rapidSW.sf > RapidSpan && !isReloading))
            {
                return;
            }
            gun.Shot();
            rapidSW.Restart();
        }

        void Reload()
        {
            ReloadProgress = reloadSW.sf / time2reload;

            if (!isReloading)
            {
                // リロード時間=消費弾数の割合*n秒
                time2reload = (1 - ammo.Ratio) * MaxReloadTime;
            }

            if (Feed.Down(Values.Key.Reload))
            {
                PreReloadRatio = ammo.Ratio;
                ammo.Reload();
                isReloading = true;
            }

            if (isReloading)
            {
                reloadSW.Start();
                if (reloadSW.SecondF() >= time2reload)
                {
                    isReloading = false;
                    reloadSW.Reset();
                }
            }
        }

        void Dead()
        {
            if (playerHP.IsZero)
            {
                // WriteData.RunOnce(() => manager.PlayerIsDead());
                a.RunOnce(() =>
                {
                    Score.ResetTimer();
                    MyScene.Load();
                });
            }
        }

        void Move()
        {
            if (!manager.Ctrlable)
            {
                return;
            }

            transform.ClampPosition2(-7.95f, 8.2f, -4.12f, 4.38f);

            (float h, float v) axis = (Input.GetAxisRaw(Constant.Horizontal), Input.GetAxisRaw(Constant.Vertical));
            transform.Translate(new Vector2(axis.h, axis.v) * movingSpeed * Time.deltaTime);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.UpgradeItem) && upgradeCount < 3)
            {
                sidegun.Add(upgradeCount++);

                // TODO make fx
                info.Destroy();
                return;
            }

            if (!parry.IsParry)
            {
                sw.Restart();
                playerSR.SetColor(Color.red);
            }
        }
    }
}
