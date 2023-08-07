using System.Collections;
using UnityEngine;
using DG.Tweening;
using Self.Utils;

namespace Self.Game
{
    public class Boss : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;

        [SerializeField]
        [Tooltip("0: charger\n1: lilc\n2: bigc\n3: spide\n4: ninja\n5: ring")]
        /// <summary>0: charger<br/>1: lilc<br/>2: bigc<br/>3: spide<br/>4: ninja<br/>5: ring</summary>
        GameObject[] mobs;

        [SerializeField]
        [Tooltip("銃口")]
        GameObject point;

        [SerializeField]
        BossUI bossUI;

        [SerializeField]
        DieMenu finishedPanel;

        GameManager manager;

        /// <summary>
        /// 初期値たち
        /// </summary>
        readonly (Quaternion rotation, Vector3 position, Vector3 scale, Color color) initial = (
            rotation: Quaternion.Euler(0, 0, 0),
            position: new(7.75f, 0, 1),
            scale: new(3, 3, 3),
            color: Color.green
        );

        PolygonCollider2D col;
        bool colTrigger = true;

        SpriteRenderer sr;

        /// <summary>
        /// HPたち
        /// </summary>
        (HP hp, int remain) boss, player;

        int currentActiveLevel = 0;
        /// <summary>
        /// アクティブなレベル
        /// </summary>
        public int CurrentActiveLevel => currentActiveLevel;

        (ushort First, ushort Second, ushort Third, ushort Fourth, ushort Fifth) Level => (0, 1, 2, 3, 4);

        bool isStarted = false;
        /// <summary>
        /// ボス戦(ウェーブ3)が始まったらTrue
        /// </summary>
        public bool IsStarted => isStarted;

        /// <summary>
        /// レベル1 のやつ
        /// </summary>
        readonly (int Bullets, float Range) Level1Rapids = (5, 1.25f);

        /// <summary>
        /// 初期座標に向かうときの移動速度
        /// </summary>
        const float PosLerpSpeed = 5;

        /// <summary>
        /// spideのスポーン間隔計測用ストップウォッチ
        /// </summary>
        readonly Stopwatch spideSW = new();

        /// <summary>
        /// ホーミング弾連射用ストップウォッチ
        /// </summary>
        readonly Stopwatch hormingSW = new();

        /// <summary>
        /// 連射速度, 出現時間
        /// </summary>
        (float spide, float[] horming) span = (
            spide: 5,
            horming: new float[] { 2f, 1.5f, 1.3f, 1f, 0.8f }
        );

        /// <summary>
        /// レベル1の連射間隔
        /// </summary>
        readonly Runner Lv1C = new();

        /// <summary>
        /// 弾幕総合
        /// </summary>
        readonly struct Barrage
        {
            public static int bulletCount = 100;
            public static Stopwatch stopwatch = new();
            public static Runner runner = new();
            public static bool during = false;
            public static float rotate = 1;
            public static float basis = 30;
            public static float rapid = 0.2f;
            public static (float min, float max) range = (60, 120);
        }

        /// <summary>
        /// 角度調整しやすいようにちょっと傾ける
        /// </summary>
        Quaternion BarrageRotationOffset => Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90);

        /// <summary>
        /// 終了用
        /// </summary>
        readonly Runner terminate = new();

        /// <summary>
        /// 最大HP
        /// </summary>
        const int MaxHP = 3000;

        /// <summary>
        /// ninjaが生成可能ならtrue
        /// </summary>
        bool ninjable = true;

        void Start()
        {
            col = GetComponent<PolygonCollider2D>();
            col.isTrigger = true;

            player.hp = Gobject.GetWithTag<HP>(Constant.Player);
            manager = Gobject.GetWithTag<GameManager>(Constant.Manager);

            transform.SetPosition(x: 12f);
            transform.SetRotation(initial.rotation);
            transform.SetScale(initial.scale);

            Stopwatch.Start(spideSW, hormingSW);
        }

        void OnEnable()
        {
            boss.hp = GetComponent<HP>();
            boss.hp.SetMax(MaxHP);
            boss.hp.Reset();

            sr = GetComponent<SpriteRenderer>();
            sr.color = initial.color;

            transform.DOMove(initial.position, PosLerpSpeed).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Both();
            Dead();

            bossUI.UpdateBossUI();
        }

        /// <summary>
        /// 死亡処理
        /// </summary>
        void Dead()
        {
            if (boss.hp.IsZero)
            {
                terminate.RunOnce(() => manager.End());
            }
        }

        /// <summary>
        /// 全般
        /// </summary>
        void Both()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            if (!Coordinate.Twins(transform.position, initial.position))
            {
                if (boss.hp.Now != boss.hp.Max)
                {
                    boss.hp.Reset();
                }

                return;
            }

            isStarted = true;

            if (colTrigger)
            {
                col.isTrigger = false;
                colTrigger = false;
            }

            boss.remain = Numeric.Percent(boss.hp.Ratio);
            player.remain = Numeric.Percent(player.hp.Ratio);

            SpawnSpide();
            SpawnHoming();

            Lv1();
            Lv2();
            Lv3();
            Lv4();
            Lv5();
        }

        void Lv1()
        {
            if (!IsCurrentActiveLevel(Level.First))
            {
                return;
            }

            currentActiveLevel = 0;

            Lv1C.RunOnce(() => StartCoroutine(Lv01()));
        }

        IEnumerator Lv01()
        {
            while (IsCurrentActiveLevel(Level.First))
            {
                yield return new WaitForSeconds(Level1Rapids.Range * 1.5f);

                for (ushort count = 0; count < Level1Rapids.Bullets; count++)
                {
                    bullets[0].Generate(point.transform.position);
                    yield return new WaitForSeconds(Level1Rapids.Range / Level1Rapids.Bullets);
                }
            }
        }

        void Lv2()
        {
            if (!IsCurrentActiveLevel(Level.Second))
            {
                return;
            }

            currentActiveLevel = 1;

            Barrage.runner.RunOnce(() =>
            {
                Barrage.during = true;
                point.transform.eulerAngles = new(0, 0, 120);
                Barrage.stopwatch.Start();
            });

            if (Barrage.during)
            {
                for (ushort count = 0; count < Barrage.bulletCount && Barrage.stopwatch.sf > Barrage.rapid; count++)
                {
                    bullets[1].Generate(point.transform.position, BarrageRotationOffset);
                    Barrage.stopwatch.Restart();

                    if (count >= 100)
                    {
                        Barrage.during = false;
                        point.transform.eulerAngles = Vector3.zero;
                        break;
                    }
                }

                float anglez = point.transform.eulerAngles.z;
                if (anglez > Barrage.range.max || anglez < Barrage.range.min)
                {
                    // 逆回転
                    Barrage.rotate *= -1;
                }

                Vector3 rotate = Barrage.basis * Barrage.rotate * Coordinate.Z;
                point.transform.Rotate(rotate * Time.deltaTime);
            }
        }

        /// <summary>
        /// Lv3で出したninjaをいれておくための
        /// </summary>
        GameObject ninjaObj;

        void Lv3()
        {
            if (!IsCurrentActiveLevel(Level.Third))
            {
                return;
            }

            currentActiveLevel = 2;

            if (ninjable)
            {
                float spawnPosX = Rand.Float(player.hp.gameObject.transform.position.x, 5);
                ninjaObj = mobs[4].Generate(new(spawnPosX, transform.position.y));

                ninjable = false;
            }

            if (ninjaObj == null)
            {
                ninjable = true;
            }
        }

        readonly Runner special = new();
        void Lv4()
        {
            if (!IsCurrentActiveLevel(Level.Fourth))
            {
                return;
            }

            currentActiveLevel = 3;

            special.RunOnce(() =>
            {
                if (Gobject.TryGetWithTag(out Player player, Constant.Player))
                {
                    if (player.CurrentGunGrade == 1 &&
                        mobs[3].Generate().TryGetComponent(out Spide spide))
                    {
                        int activate = Lottery.Weighted(1, 25, 50);
                        spide.SetLevel(activate);
                    }
                }
            });
        }

        (float Span, Stopwatch stopwatch) level5Spawns = (Span: 1.3f, stopwatch: new());
        readonly Runner l5runner = new();

        void Lv5()
        {
            if (!IsCurrentActiveLevel(Level.Fifth))
            {
                return;
            }

            currentActiveLevel = 4;

            l5runner.RunOnce(() => level5Spawns.stopwatch.Start());

            if (level5Spawns.stopwatch.sf > boss.hp.Ratio * level5Spawns.Span)
            {
                int index = Rand.Int(0, mobs.Length - 1);
                mobs[index].Generate(transform.position, Quaternion.identity);

                level5Spawns.stopwatch.Restart();
            }
        }

        /// <summary>
        /// ホーミング弾生成
        /// </summary>
        void SpawnHoming()
        {
            if (hormingSW.sf > span.horming[currentActiveLevel])
            {
                bullets[4].Generate(transform.position);
                hormingSW.Restart();
            }
        }

        /// <summary>
        /// spide生成
        /// </summary>
        void SpawnSpide()
        {
            if (spideSW.SecondF() >= span.spide)
            {
                int level = Lottery.Weighted(1, 0.5f, 0.25f);
                mobs[3].Generate().GetComponent<Spide>().SetLevel(level);

                span.spide = Rand.Int(20, 30);
                spideSW.Restart();
            }
        }

        /// <summary>
        /// レベル_levelがアクティブならTrue
        /// </summary>
        public bool IsCurrentActiveLevel(ushort level)
        {
            ushort[] borders = { 100, 80, 60, 40, 20 };

            return level switch
            {
                0 => boss.remain >= borders[0],
                1 => boss.remain >= borders[1] && boss.remain < borders[0],
                2 => boss.remain >= borders[2] && boss.remain < borders[1],
                3 => boss.remain >= borders[3] && boss.remain < borders[2],
                4 => boss.remain >= borders[4] && boss.remain < borders[3],
                _ => throw new System.Exception(),
            };
        }

        /// <summary>
        /// 残り体力によって目の色をかえる
        /// </summary>
        public void UpdateEyeColor()
        {
            // 100 ≧ hue ≧ 0
            float hue = boss.hp.Ratio / 360 * 100;
            sr.color = Color.HSVToRGB(hue, 1, 1);
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Bullet))
            {
                UpdateEyeColor();
                bossUI.UpdateBossUI();
            }
        }
    }
}
