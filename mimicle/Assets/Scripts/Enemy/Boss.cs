using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnionEngine.Extend;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace UnionEngine
{
    public class Boss : MonoBehaviour
    {
        [SerializeField, Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;

        [SerializeField, Tooltip("0: charger\n1: lilc\n2: slilc\n3: spide")]
        GameObject[] mobs;
        enum Mobs
        {
            Charger,
            LilC,
            BigC,
            Spide
        }

        [SerializeField]
        GameObject point;

        [SerializeField]
        BossUI bossUI;

        [System.Serializable]
        struct Colour
        {
            public int hpBorder;
            public Color color;
        }
        [SerializeField]
        Colour[] colour = new Colour[5];

        readonly (Quaternion Rotation, Vector3 Position, Vector3 Scale) initial = (
            Quaternion.Euler(0, 0, 0), new(7.75f, 0, 1), new(3, 3, 3));

        new PolygonCollider2D collider;
        bool collide = true;

        SpriteRenderer sr;
        // HP bossHp, playerHp;
        // int bossRemain = 0, playerRemain = 0;
        (HP hp, int remain) boss, player;

        int activeLevel = 0;
        public int ActiveLevel => activeLevel;
        enum Level { First = 0, Second, Third, Fourth, Fifth }

        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;
        Stopwatch spideSW = new(), l1SW = new(), l2SW = new();
        float spawnSpideSpan = 5;

        // const float BarrageRapid = 0.2f;
        (int Bullets, float Range) W1Rapid = (5, 1.25f);

        (float rotate, float basis, float rapid) barrageSpeed = (1, 30f, 0.2f);
        // float rotate = 1;
        // float baseSpeed = 30f;
        float posLerpSpeed = 5;

        void Start()
        {
            bossUI ??= GameObject.Find("Canvas").GetComponent<BossUI>();
            collider = GetComponent<PolygonCollider2D>();
            collider.isTrigger = true;
            sr = GetComponent<SpriteRenderer>();
            sr.color = colour[((int)Level.First)].color;
            player.hp = Gobject.Find(Constant.Player).GetComponent<HP>();
            boss.hp = GetComponent<HP>();
            boss.hp.SetMax();
            transform.SetRotation(initial.Rotation);
            transform.SetScale(initial.Scale);
            spideSW.Start();
        }

        void OnEnable()
        {
            transform.DOMove(initial.Position, posLerpSpeed).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Debug.DrawLine(point.transform.position, point.transform.up * 5, Color.HSVToRGB(Time.time % 1, 1, 1));
            Both();
            if (boss.hp.IsZero)
            {
                Site.Load(Constant.Final);
            }
        }

        void Both()
        {
            if (!Coordinate.Twins(transform.position, initial.Position))
            {
                return;
            }

            startBossBattle = true;
            if (collide)
            {
                collider.isTrigger = false;
                collide = false;
            }

            boss.remain = Numeric.Percent(boss.hp.Ratio);
            player.remain = Numeric.Percent(player.hp.Ratio);

            SpawnSpide();

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
                yield return new WaitForSeconds(W1Rapid.Range * 1.5f);
                for (var i = 0; i < W1Rapid.Bullets; i++)
                {
                    bullets[0].Instance(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(W1Rapid.Range / W1Rapid.Bullets);
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
            o5.RunOnce(() => StartCoroutine(Lv5s()));
        }

        IEnumerator Lv5s()
        {
            while (isActiveLevel(((int)Level.Fifth)))
            {
                yield return new WaitForSeconds(5);
                bullets[4].Instance(point.transform.position, Quaternion.identity);
            }
        }

        void SpawnSpide()
        {
            if (spideSW.SecondF() >= spawnSpideSpan)
            {
                var spide = mobs[((int)Mobs.Spide)].Instance();
                spide.GetComponent<Spide>().SetLevel(Lottery.ChoiceByWeights(1, 0.5f, 0.25f));
                spideSW.Restart();
                spawnSpideSpan = Rnd.Int(20, 30);
            }
        }

        public void ChangeBodyColor()
        {
            foreach (var i in colour)
            {
                if (boss.remain >= i.hpBorder)
                {
                    sr.color = i.color;
                    break;
                }
            }
        }

        public bool isActiveLevel(int _level)
        {
            switch (_level)
            {
                case 0:
                    return boss.remain >= colour[((int)Level.First)].hpBorder;
                case 1:
                    return boss.remain >= colour[((int)Level.Second)].hpBorder && boss.remain < colour[((int)Level.First)].hpBorder;
                case 2:
                    return boss.remain >= colour[((int)Level.Third)].hpBorder && boss.remain < colour[((int)Level.Second)].hpBorder;
                case 3:
                    return boss.remain >= colour[((int)Level.Fourth)].hpBorder && boss.remain < colour[((int)Level.Third)].hpBorder;
                case 4:
                    return boss.remain >= colour[((int)Level.Fifth)].hpBorder && boss.remain < colour[((int)Level.Fourth)].hpBorder;
                default: throw new System.Exception();
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(Constant.Bullet))
            {
                ChangeBodyColor();
                bossUI.UpdateBossUI();
            }
        }
    }
}
