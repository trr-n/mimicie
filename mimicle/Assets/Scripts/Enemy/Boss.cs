using System.Collections;
using UnityEngine;
using DG.Tweening;
using MyGame.Utils;

namespace MyGame
{
    public class Boss : MonoBehaviour
    {
        [SerializeField, Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;

        [SerializeField, Tooltip("0: charger\n1: lilc\n2: bigc\n3: spide")]
        GameObject[] mobs;
        readonly (int charger, int lilc, int bigc, int spide) Mobs = (0, 1, 2, 3);

        [SerializeField]
        GameObject point;

        [SerializeField]
        BossUI bossUI;

        [SerializeField]
        DieMenu finishedPanel;

        [SerializeField]
        GameManager manager;


        /// <summary>
        /// 初期値たち
        /// </summary>
        readonly (Quaternion rotation, Vector3 position, Vector3 scale, Color color) initial = (
            Quaternion.Euler(0, 0, 0), new(7.75f, 0, 1), new(3, 3, 3), Color.green);

        new PolygonCollider2D collider;
        bool collide = true;

        SpriteRenderer bossr;
        (HP hp, int remain) self, player;

        int activeLevel = 0;
        /// <summary>
        /// アクティブなレベル
        /// </summary>
        public int ActiveLevel => activeLevel;
        enum Level { First = 0, Second, Third, Fourth, Fifth }

        bool startBossBattle = false;
        /// <summary>
        /// ボス戦が始まったらTrue
        /// </summary>
        public bool StartBossBattle => startBossBattle;

        /// <summary>
        /// レベル1 のやつ
        /// </summary>
        readonly (int Bullets, float Range) w1Rapid = (5, 1.25f);

        (float rotate, float basis, float rapid) barrageSpeed = (1, 30f, 0.2f);

        /// <summary>
        /// 初期座標に向かうときの移動速度
        /// </summary>
        float posLerpSpeed = 5;

        /// <summary>
        /// spideのスポーン間隔計測用ストップウォッチ
        /// </summary>
        Stopwatch spideSW = new();

        /// <summary>
        /// ホーミング弾連射用ストップウォッチ
        /// </summary>
        Stopwatch hormingSW = new();

        /// <summary>
        /// 連射速度, 出現時間
        /// </summary>
        (float spide, float[] horming) span = (spide: 5, horming: new float[] { 5.5f, 4.5f, 4f, 3f, 2f });

        /// <summary>
        /// レベル1の連射間隔
        /// </summary>
        One Lv1C = new();

        // /// <summary>
        // /// 弾幕用フラグ
        // /// </summary>
        // bool isBarrage = false;

        /// <summary>
        /// 弾幕総合
        /// </summary>
        (int bulletCount, Stopwatch sw, One runner, bool during) barrage = (100, new(), new(), false);

        void Start()
        {
            collider = GetComponent<PolygonCollider2D>();
            collider.isTrigger = true;

            player.hp = Gobject.Find(Constant.Player).GetComponent<HP>();

            transform.position = new(12, 0, 0);
            transform.SetRotation(initial.rotation);
            transform.SetScale(initial.scale);

            Stopwatch.Start(spideSW, hormingSW);
        }

        void OnEnable()
        {
            self.hp = GetComponent<HP>();
            self.hp.SetMax();

            bossr = GetComponent<SpriteRenderer>();
            bossr.color = initial.color;

            transform.DOMove(initial.position, posLerpSpeed).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Both();
            Dead();
        }

        /// <summary>
        /// 死亡処理
        /// </summary>
        void Dead()
        {
            if (self.hp.IsZero)
            {
                // MyScene.Load(Constant.Final);
                finishedPanel.SetText(isDead: false);
                manager.PlayerIsDead();
            }
        }

        /// <summary>
        /// 全般
        /// </summary>
        void Both()
        {
            if (!Coordinate.Twins(transform.position, initial.position))
            {
                return;
            }

            startBossBattle = true;
            if (collide)
            {
                collider.isTrigger = false;
                collide = false;
            }

            self.remain = Numeric.Percent(self.hp.Ratio);
            player.remain = Numeric.Percent(player.hp.Ratio);

            SpawnSpide();
            SpawnHoming();

            Lv1();
            Lv2();
            Lv3();
            Lv4();
            Lv5();
        }

        /// <summary>
        /// 75 ~ 100, blue: 5% not homing, fire every second 
        /// </summary>
        void Lv1()
        {
            if (!isActiveLevel(((int)Level.First)))
            {
                return;
            }
            activeLevel = 0;

            Lv1C.RunOnce(() => StartCoroutine(Lv01()));
        }

        IEnumerator Lv01()
        {
            while (isActiveLevel(((int)Level.First)))
            {
                yield return new WaitForSeconds(w1Rapid.Range * 1.5f);
                for (int count = 0; count < w1Rapid.Bullets; count++)
                {
                    bullets[0].Generate(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(w1Rapid.Range / w1Rapid.Bullets);
                }
            }
        }

        /// <summary>
        /// 弾幕
        /// </summary>
        void Lv2()
        {
            if (!isActiveLevel(((int)Level.Second)))
            {
                return;
            }
            activeLevel = 1;

            barrage.runner.RunOnce(() =>
            {
                barrage.during = true;
                point.transform.eulerAngles = new(0, 0, 120);
                barrage.sw = new(true);
            });

            (float Max, float Min) range = (120, 60);

            if (barrage.during)
            {
                for (int count = 0; count < barrage.bulletCount && barrage.sw.sf > barrageSpeed.rapid; count++)
                {
                    bullets[1].Generate(point.transform.position, Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90));
                    barrage.sw.Restart();

                    if (count >= 100)
                    {
                        barrage.during = false;
                        point.transform.eulerAngles = Vector3.zero;
                        break;
                    }
                }

                if (point.transform.eulerAngles.z > range.Max || point.transform.eulerAngles.z < range.Min)
                {
                    // 逆回転
                    barrageSpeed.rotate *= -1;
                }

                point.transform.Rotate(new Vector3(0, 0, barrageSpeed.basis * barrageSpeed.rotate * Time.deltaTime));
            }
        }

        One StopBarrage = new();
        /// <summary>
        /// 30 ~ 50, yellow: 9% homing
        /// </summary>
        void Lv3()
        {
            if (!isActiveLevel(((int)Level.Third)))
            {
                return;
            }

            activeLevel = 2;
        }

        /// <summary>
        /// 10 ~ 30, orange: 13% homing
        /// </summary>
        void Lv4()
        {
            if (!isActiveLevel(((int)Level.Fourth)))
            {
                return;
            }
            activeLevel = 3;
        }

        /// <summary>
        /// 00 ~ 10, red: 15% homing
        /// </summary>
        void Lv5()
        {
            if (!isActiveLevel(((int)Level.Fifth)))
            {
                return;
            }

            activeLevel = 4;
        }

        /// <summary>
        /// ホーミング弾生成
        /// </summary>
        void SpawnHoming()
        {
            if (hormingSW.sf > span.horming[activeLevel])
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
                var spide = mobs[Mobs.spide].Generate();
                spide.GetComponent<Spide>().SetLevel(Lottery.Weighted(1, 0.5f, 0.25f));
                // spawnSpideSpan = Rnd.Int(20, 30);
                span.spide = Rnd.Int(20, 30);
                spideSW.Restart();
            }
        }

        /// <summary>
        /// 残り体力によって目の色をかえる
        /// </summary>
        void UpdateEyeColor()
        {
            // 100 ≧ hue ≧ 0
            var hue = self.hp.Ratio / 360 * 100;
            bossr.color = Color.HSVToRGB(hue, 1, 1);
        }

        /// <summary>
        /// レベル_levelがアクティブならTrue
        /// </summary>
        public bool isActiveLevel(int _level)
        {
            int[] borders = { 100, 80, 60, 40, 20 };
            switch (_level)
            {
                case 0:
                    return self.remain >= borders[0];
                case 1:
                    return self.remain >= borders[1] && self.remain < borders[0];
                case 2:
                    return self.remain >= borders[2] && self.remain < borders[1];
                case 3:
                    return self.remain >= borders[3] && self.remain < borders[2];
                case 4:
                    return self.remain >= borders[4] && self.remain < borders[3];
                default: throw new System.Exception();
            }
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
