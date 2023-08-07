using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
    {
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
        Image fadePanel;

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
        [NonSerialized]
        public float ReloadProgress;

        /// <summary>
        /// 移動速度
        /// </summary> 
        readonly (float basis, float reduct, float shoot) move = (5, 0.9f, 0.7f);

        /// <summary>
        /// プレイヤーのHP
        /// </summary>
        HP playerHP;

        /// <summary>
        /// リロード用ストップウォッチ
        /// </summary>
        readonly Stopwatch reloadSW = new();

        /// <summary>
        /// 連射用ストップウォッチ
        /// </summary>
        readonly Stopwatch rapidSW = new(true);

        /// <summary>
        /// 被弾した時の色変更用ストップウォッチ
        /// </summary>
        readonly Stopwatch sw = new();

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
        [NonSerialized]
        public bool NotNinnin = false;

        RaycastHit2D hit;
        /// <summary>
        /// レーザーの着弾地点表示用
        /// </summary>
        public RaycastHit2D Hit => hit;

        /// <summary>
        /// リロード時間
        /// </summary>
        public float Reload__ => reloadSW.SecondF(2);

        /// <summary>
        /// 最大リロード時間
        /// </summary>
        const float MaxReloadTime = 1f;

        /// <summary>
        /// 残弾数の割合
        /// </summary>
        [NonSerialized]
        public float PreReloadRatio = 0f;

        /// <summary>
        /// セーブデータ書き込み用
        /// </summary>
        readonly Runner write = new();

        ushort currentGunGrade = 0;
        /// <summary>
        /// 現在の銃のグレード
        /// </summary>
        public ushort CurrentGunGrade => currentGunGrade;

        /// <summary>
        /// 0: normal<br/> 1: rocket launcher
        /// </summary>
        readonly float[] rates = { 0.1f, 1f };

        Ammo ammo;

        AudioSource speaker;

        readonly int detectLayers = 1 << 9 | 1 << 10;

        void Awake()
        {
            manager = Gobject.GetWithTag<GameManager>(Constant.Manager);

            speaker = GetComponent<AudioSource>();

            playerHP = GetComponent<HP>();
            playerSR = GetComponent<SpriteRenderer>();
            playerCol = GetComponent<BoxCollider2D>();

            ammo = Gobject.GetWithTag<Ammo>(transform.GetChild(0).gameObject);
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

            hit = Physics2D.Raycast(transform.position, Vector2.right, 20.48f, detectLayers);
            playerCol.isTrigger = parry.IsParry;

            if (sw.sf >= 0.2f)
            {
                playerSR.color = Color.white;
            }

            Shot(gun.Grade switch
            {
                2 => rates[gun.Mode],
                _ => rates[gun.Grade]
            });
        }

        /// <summary>
        /// 発砲
        /// </summary>
        void Shot(float rapid)
        {
            if (NotNinnin = !(
                Inputs.Pressed(Constant.Fire) && !isReloading && !ammo.IsZero && rapidSW.sf > rapid))
            {
                return;
            }

            gun.Shot(currentGunGrade);
            rapidSW.Restart();
        }

        /// <summary>
        /// リロード
        /// </summary>
        void Reload()
        {
            ReloadProgress = reloadSW.sf / time2reload;

            if (!isReloading)
            {
                // リロード時間=消費弾数の割合*n秒
                time2reload = (1 - ammo.Ratio) * MaxReloadTime;
            }

            if (Inputs.Down(Constant.Reload))
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
                write.RunOnce(() => StartCoroutine(Fade(true)));
            }
        }

        IEnumerator Fade(bool fout)
        {
            float alpha = 0f;

            while (true)
            {
                yield return null;

                alpha = Mathf.Clamp(alpha, 0, 1);
                alpha = fout ? alpha += Time.deltaTime : alpha -= Time.deltaTime;

                // fadePanel.color.SetAlpha(alpha);
                fadePanel.color = new(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);

                if (alpha >= 1)
                {
                    break;
                }
            }

            Score.ResetTimer();
            MyScene.Load();
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        void Move()
        {
            if (!manager.Ctrlable)
            {
                return;
            }

            transform.ClampPosition2(-7.95f, 8.2f, -4.12f, 4.38f);

            Vector2 move = new(Input.GetAxisRaw(Constant.Horizontal), Input.GetAxisRaw(Constant.Vertical));
            transform.Translate(Time.deltaTime * this.move.basis * move);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.UpgradeItem) && currentGunGrade <= 2)
            {
                currentGunGrade++;
                info.Destroy();

                return;
            }

            if (!parry.IsParry)
            {
                try
                {
                    speaker.RandomPlayOneShot(damageSE);
                }
                catch { }
                sw.Restart();
                playerSR.SetColor(Color.red);
            }
        }
    }
}
