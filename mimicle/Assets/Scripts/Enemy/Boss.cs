using System.Collections;
using UnityEngine;
using DG.Tweening;
using Self.Utils;
using System.Collections.Generic;

namespace Self
{
    public class Boss : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;

        [SerializeField]
        [Tooltip("0: charger\n1: lilc\n2: bigc\n3: spide")]
        /// <summary>
        /// 0: charger<br/>
        /// 1: lilc<br/>
        /// 2: bigc<br/>
        /// 3: spide<br/>
        /// 4: ninja<br/>
        /// </summary>
        GameObject[] mobs;

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
        (Quaternion rotation, Vector3 position, Vector3 scale, Color color) initial => (
            Quaternion.Euler(0, 0, 0), new(7.75f, 0, 1), new(3, 3, 3), Color.green);

        new PolygonCollider2D collider;
        bool collide = true;

        SpriteRenderer bossr;

        /// <summary>
        /// HPたち
        /// </summary>
        (HP hp, int remain) boss, player;

        int currentActiveLevel = 0;
        /// <summary>
        /// アクティブなレベル
        /// </summary>
        public int CurrentActiveLevel => currentActiveLevel;
        enum Level { First = 0, Second, Third, Fourth, Fifth }

        bool isStartedBossBattle = false;
        /// <summary>
        /// ボス戦(ウェーブ3)が始まったらTrue
        /// </summary>
        public bool IsStartedBossBattle => isStartedBossBattle;

        /// <summary>
        /// レベル1 のやつ
        /// </summary>
        (int Bullets, float Range) level1Rapids => (5, 1.25f);

        /// <summary>
        /// 初期座標に向かうときの移動速度
        /// </summary>
        float PosLerpSpeed = 5;

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

        /// <summary>
        /// 弾幕総合
        /// </summary>
        (int bulletCount, Stopwatch stopwatch, One runner, bool during, (float rotate, float basis, float rapid) speed) barrage = (
            bulletCount: 100,
            stopwatch: new(),
            runner: new(),
            during: false,
            speed: (rotate: 1, basis: 30f, rapid: 0.2f)
        );

        /// <summary>
        /// 角度調整しやすいようにちょっと傾ける
        /// </summary>
        Quaternion BarrageRotationOffset => Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90);

        /// <summary>
        /// 終了用
        /// </summary>
        One terminated = new();

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
            boss.hp = GetComponent<HP>();
            boss.hp.SetMax();

            bossr = GetComponent<SpriteRenderer>();
            bossr.color = initial.color;

            transform.DOMove(initial.position, PosLerpSpeed).SetEase(Ease.OutCubic);
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
            if (boss.hp.IsZero)
            {
                terminated.RunOnce(() => manager.End());
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

            isStartedBossBattle = true;
            if (collide)
            {
                collider.isTrigger = false;
                collide = false;
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

        /// <summary>
        /// 75 ~ 100, blue: 5% not homing, fire every second 
        /// </summary>
        void Lv1()
        {
            if (!isActiveLevel(((int)Level.First)))
            {
                return;
            }

            currentActiveLevel = 0;

            Lv1C.RunOnce(() => StartCoroutine(Lv01()));
        }

        IEnumerator Lv01()
        {
            while (isActiveLevel(((int)Level.First)))
            {
                yield return new WaitForSeconds(level1Rapids.Range * 1.5f);

                for (int count = 0; count < level1Rapids.Bullets; count++)
                {
                    bullets[0].Generate(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(level1Rapids.Range / level1Rapids.Bullets);
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

            currentActiveLevel = 1;

            barrage.runner.RunOnce(() =>
            {
                barrage.during = true;
                point.transform.eulerAngles = new(0, 0, 120);
                barrage.stopwatch.Start();
            });

            (float Max, float Min) range = (120, 60);

            if (barrage.during)
            {
                for (int count = 0; count < barrage.bulletCount && barrage.stopwatch.sf > barrage.speed.rapid; count++)
                {
                    bullets[1].Generate(point.transform.position, BarrageRotationOffset);
                    barrage.stopwatch.Restart();

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
                    barrage.speed.rotate *= -1;
                }

                point.transform.Rotate(new Vector3(0, 0, barrage.speed.basis * barrage.speed.rotate * Time.deltaTime));
            }
        }

        bool ninjable = true;
        List<GameObject> ninjas = new();
        /// <summary>
        /// 30 ~ 50, yellow: 9% homing
        /// </summary>
        void Lv3()
        {
            if (!isActiveLevel(((int)Level.Third)))
            {
                return;
            }

            currentActiveLevel = 2;

            // 2体以上忍者が湧かないように
            if (ninjable)
            {
                float spanwPosX = Rnd.Float(player.hp.gameObject.transform.position.x, 5);
                ninjas.Add(mobs[4].Generate(new Vector2(spanwPosX, transform.position.y), Quaternion.identity));

                ninjable = false;
            }

            if (ninjas.Count <= 0)
            {
                ninjable = true;
            }
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

            currentActiveLevel = 3;
        }

        (float Span, Stopwatch stopwatch) level5Spawns = (Span: 1.3f, stopwatch: new());
        One level5StopwatchRunner = new();
        /// <summary>
        /// 00 ~ 10, red: 15% homing
        /// </summary>
        void Lv5()
        {
            if (!isActiveLevel(((int)Level.Fifth)))
            {
                return;
            }

            currentActiveLevel = 4;

            level5StopwatchRunner.RunOnce(() => level5Spawns.stopwatch.Start());

            if (level5Spawns.stopwatch.sf > boss.hp.Ratio * level5Spawns.Span)
            {
                int index = Lottery.Weighted();
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
                mobs[0].Generate().GetComponent<Spide>().SetLevel(Lottery.Weighted(1, 0.5f, 0.25f));
                span.spide = Rnd.Int(20, 30);

                spideSW.Restart();
            }
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
                    return boss.remain >= borders[0];

                case 1:
                    return boss.remain >= borders[1] && boss.remain < borders[0];

                case 2:
                    return boss.remain >= borders[2] && boss.remain < borders[1];

                case 3:
                    return boss.remain >= borders[3] && boss.remain < borders[2];

                case 4:
                    return boss.remain >= borders[4] && boss.remain < borders[3];

                default: throw new System.Exception();
            }
        }

        /// <summary>
        /// 残り体力によって目の色をかえる
        /// </summary>
        void UpdateEyeColor()
        {
            // 100 ≧ hue ≧ 0
            float hue = boss.hp.Ratio / 360 * 100;
            bossr.color = Color.HSVToRGB(hue, 1, 1);
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
