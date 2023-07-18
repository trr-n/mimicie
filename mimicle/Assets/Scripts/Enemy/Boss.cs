using System.Collections;
using UnityEngine;
using DG.Tweening;
using Feather.Utils;

namespace Feather
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


        readonly (Quaternion rotation, Vector3 position, Vector3 scale, Color color) initial = (
            Quaternion.Euler(0, 0, 0), new(7.75f, 0, 1), new(3, 3, 3), Color.green);

        new PolygonCollider2D collider;
        bool collide = true;

        SpriteRenderer bossr;
        (HP hp, int remain) self, player;

        int activeLevel = 0;
        public int ActiveLevel => activeLevel;
        enum Level { First = 0, Second, Third, Fourth, Fifth }

        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;

        readonly (int Bullets, float Range) w1Rapid = (5, 1.25f);

        (float rotate, float basis, float rapid) barrageSpeed = (1, 30f, 0.2f);
        float posLerpSpeed = 5;

        Stopwatch spideSW = new();
        // float spawnSpideSpan = 5;

        Stopwatch hormingSW = new();
        // float[] spawnHomingSpan => new float[] { 5.5f, 4.5f, 4f, 3f, 2f };

        (float spide, float[] horming) span = (
            spide: 5,
            horming: new float[] { 5.5f, 4.5f, 4f, 3f, 2f }
        );

        void Start()
        {
            // bossUI ??= GameObject.Find("Canvas").GetComponent<BossUI>();

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

            Save.Read<ScoreData>(out data, "goigoisu-", Application.dataPath + "/score.sav");
        }
        ScoreData data;

        void Update()
        {
            Both();
            Dead();
            print(data.time.Space() + data.score);
        }

        void Dead()
        {
            if (self.hp.IsZero)
            {
                // MyScene.Load(Constant.Final);
                finishedPanel.SetText(isDead: false);
                manager.PlayerIsDead();
            }
        }

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

        bool once = false;
        Stopwatch l1sw = new();
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
            if (!once)
            {
                StartCoroutine(Lv01());
                once = true;
            }
        }

        IEnumerator Lv01()
        {
            while (isActiveLevel(((int)Level.First)))
            {
                yield return new WaitForSeconds(w1Rapid.Range * 1.5f);
                for (var i = 0; i < w1Rapid.Bullets; i++)
                {
                    bullets[0].Instance(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(w1Rapid.Range / w1Rapid.Bullets);
                }
            }
        }

        One barrage = new();
        bool isBarrage = false;
        Stopwatch barrageSW;
        /// <summary>
        /// barrage
        /// </summary>
        void Lv2()
        {
            if (!isActiveLevel(((int)Level.Second)))
            {
                return;
            }
            activeLevel = 1;

            barrage.RunOnce(() =>
            {
                isBarrage = true;
                point.transform.eulerAngles = new(0, 0, 120);
                barrageSW = new(true);
            });

            (float Max, float Min) Range = (120, 60);
            if (isBarrage)
            {
                for (int i = 0; i < 100 && barrageSW.sf > barrageSpeed.rapid; i++)
                {
                    bullets[1].Instance(point.transform.position, Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90));
                    barrageSW.Restart();
                    if (i >= 100)
                    {
                        isBarrage = false;
                        point.transform.eulerAngles = Vector3.zero;
                        break;
                    }
                }

                if (point.transform.eulerAngles.z > Range.Max || point.transform.eulerAngles.z < Range.Min)
                {
                    // 逆回転
                    barrageSpeed.rotate *= -1;
                }
                point.transform.Rotate(new Vector3(0, 0, barrageSpeed.basis * barrageSpeed.rotate * Time.deltaTime));
            }
        }

        // 弾幕 n発
        IEnumerator Barrage()
        {
            int n = 100;
            for (int i = 0; i < n; i++)
            {
                yield return new WaitForSeconds(barrageSpeed.rapid);
                bullets[1].Instance(point.transform.position, Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90));
                print(isBarrage);
            }
            isBarrage = false;
            point.transform.eulerAngles = Vector3.zero;
        }

        One StopBarrage = new();
        /// <summary>
        ///TODO 30 ~ 50, yellow: 9% homing
        /// </summary>
        void Lv3()
        {
            if (!isActiveLevel(((int)Level.Third)))
            {
                return;
            }
            activeLevel = 2;
            // StopBarrage.RunOnce(() =>
            // {
            //     StopCoroutine(Barrage());
            // });
        }

        /// <summary>
        ///TODO 10 ~ 30, orange: 13% homing
        /// </summary>
        void Lv4()
        {
            if (!isActiveLevel(((int)Level.Fourth)))
            {
                return;
            }
            activeLevel = 3;
        }

        One o5 = new();
        /// <summary>
        ///TODO 00 ~ 10, red: 15% homing
        /// </summary>
        void Lv5()
        {
            if (!isActiveLevel(((int)Level.Fifth)))
            {
                return;
            }
            activeLevel = 4;
        }

        void SpawnHoming()
        {
            // if (homingSW.sf > spawnHomingSpan[activeLevel])
            if (hormingSW.sf > span.horming[activeLevel])
            {
                bullets[4].Instance(transform.position);
                hormingSW.Restart();
            }
        }

        void SpawnSpide()
        {
            // if (spideSW.SecondF() >= spawnSpideSpan)
            if (spideSW.SecondF() >= span.spide)
            {
                var spide = mobs[Mobs.spide].Instance();
                spide.GetComponent<Spide>().SetLevel(Lottery.ChoiceByWeights(1, 0.5f, 0.25f));
                // spawnSpideSpan = Rnd.Int(20, 30);
                span.spide = Rnd.Int(20, 30);
                spideSW.Restart();
            }
        }

        // public void ChangeBodyColor()
        // {
        //     foreach (var i in colour)
        //     {
        //         if (self.remain >= i.hpBorder)
        //         {
        //             bossr.color = i.color;
        //             break;
        //         }
        //     }
        // }

        void UpdateEyeColor()
        {
            // 100 ≧ hue ≧ 0
            var hue = self.hp.Ratio / 360 * 100;
            bossr.color = Color.HSVToRGB(hue, 1, 1);
        }

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
                // ChangeBodyColor();
                UpdateEyeColor();
                bossUI.UpdateBossUI();
            }
        }
    }
}
