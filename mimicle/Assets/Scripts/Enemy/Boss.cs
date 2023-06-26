using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using DG.Tweening;
using UnityEngine.UI;

namespace Mimical
{
    public class Boss : MonoBehaviour
    {
        [SerializeField, Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;
        [SerializeField, Tooltip("0: charger\n1: lilc\n2: slilc\n3: spide")]
        GameObject[] mobs;
        enum Mobs { Charger, LilC, SLilC, Spide }
        [SerializeField]
        GameObject point;
        [SerializeField]
        BossUI bossUI;
        [System.Serializable]
        struct Colour
        {
            public int remainHp;
            public Color color;
        }
        [SerializeField]
        Colour[] colour = new Colour[5];
        // Colour2[] colour = new Colour2[5];

        readonly (Quaternion Rotation, Vector3 Position, Vector3 Scale) Initial = (Quaternion.Euler(0, 0, 90), new(7, 0, 1), Vector3.one * 3);
        enum Level { First = 0, Second, Third, Fourth, Fifth }
        float posLerpSpeed = 5;
        new PolygonCollider2D collider;
        bool collide = true;
        SpriteRenderer sr;
        HP bossHp, playerHp;
        int bossRemain = 0, playerRemain = 0;
        int activeLevel = 0;
        public int ActiveLevel => activeLevel;
        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;
        Stopwatch spideSW = new(), l1SW = new(), l2SW = new();
        const float SpawnSpideSpan = 30;
        const float barrageRapid = 0.2f;
        (int bullets, float range) W1Rapid = (5, 1.25f);

        delegate void Waves();
        Waves waves;

        void Start()
        {
            bossUI ??= GameObject.Find("Canvas").GetComponent<BossUI>();
            collider = GetComponent<PolygonCollider2D>();
            collider.isTrigger = true;
            sr = GetComponent<SpriteRenderer>();
            sr.color = colour[((int)Level.First)].color;
            playerHp = Gobject.Find(Constant.Player).GetComponent<HP>();
            bossHp = GetComponent<HP>();
            bossHp.SetMax();
            transform.setr(Initial.Rotation);
            transform.sets(Initial.Scale);
            spideSW.Start();
        }

        void OnEnable()
        {
            transform.DOMove(Initial.Position, posLerpSpeed).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Both();
            if (bossHp.IsZero)
                Section.Load(Constant.Final);
        }

        void Both()
        {
            if (!Coordinate.Twins(transform.position, Initial.Position))
                return;
            startBossBattle = true;
            if (collide)
            {
                collider.isTrigger = false;
                collide = false;
            }
            bossRemain = Numeric.Percent(bossHp.Ratio);
            playerRemain = Numeric.Percent(playerHp.Ratio);
            SpawnSpide();
            Lv1();
            Lv2();
            Lv3();
            Lv4();
            Lv5();
            // waves += Lv1;
            // waves += Lv2;
            // waves += Lv3;
            // waves += Lv4;
            // waves += Lv5;
            // waves();
            // print($"boss: {selfRemain}%, player: {playerRemain}%");
        }

        bool once = false;
        Stopwatch l1sw = new();
        /// <summary>
        /// 75 ~ 100, 青: 5%弾, 毎秒発射
        /// </summary>
        void Lv1()
        {
            if (!isActiveLevel(((int)Level.First)))
                return;
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
                yield return new WaitForSeconds(Rnd.randint(1, 10));
                for (var i = 0; i < W1Rapid.bullets; i++)
                {
                    bullets[0].Instance(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(W1Rapid.range / W1Rapid.bullets);
                }
            }
        }

        bool l2 = true;
        /// <summary>
        //// / 50 ~ 75, 緑:  7%ホーミング弾
        /// danmaku
        /// </summary>
        void Lv2()
        {
            if (!isActiveLevel(((int)Level.Second)))
                return;
            activeLevel = 1;
            point.transform.Rotate(new(0, 0, Mathf.Sin(Time.time)));
            if (l2)
            {
                StartCoroutine(Barrage());
                l2 = false;
            }
        }
        IEnumerator Barrage()
        {
            int i = 0;
            while (i <= 100)
            {
                yield return new WaitForSecondsRealtime(barrageRapid);
                i++;
                bullets[1].Instance(point.transform.position, point.transform.rotation);
            }
        }

        /// <summary>
        /// 30 ~ 50, 黄:  9%ホーミング弾
        /// </summary>
        void Lv3()
        {
            if (!isActiveLevel(((int)Level.Third)))
                return;
            activeLevel = 2;
        }

        /// <summary>
        /// 10 ~ 30, 橙:  13%ホーミング弾
        /// </summary>
        void Lv4()
        {
            if (!isActiveLevel(((int)Level.Fourth)))
                return;
            activeLevel = 3;
        }

        bool bb = true;
        /// <summary>
        /// 00 ~ 10, 赤:  15%ホーミング弾
        /// </summary>
        void Lv5()
        {
            if (!isActiveLevel(((int)Level.Fifth)))
                return;
            activeLevel = 4;
            if (bb)
            {
                bb = false;
                StartCoroutine(Lv5s());
            }
        }

        IEnumerator Lv5s()
        {
            while (isActiveLevel(((int)Level.Fifth)))
            {
                yield return new WaitForSecondsRealtime(5);
                bullets[4].Instance(point.transform.position, Quaternion.identity);
            }
        }

        void SpawnSpide()
        {
            if (!(spideSW.SecondF() >= SpawnSpideSpan))
                return;
            var spide = mobs[((int)Mobs.Spide)].Instance();
            if (spide.TryGetComponent<Spide>(out var _spide))
            {
                var levelDict = new Dictionary<int, float>() { { 0, 50 }, { 1, 25 }, { 2, 12.5f } };
                _spide.SetLevel(Rnd.Pro(levelDict));
            }
            // _spide.SetLevel(Rnd.randint(0, 2));
            spideSW.Restart();
        }

        public void ChangeBodyColor()
        {
            foreach (var i in colour)
                if (bossRemain >= i.remainHp)
                {
                    sr.color = i.color;
                    break;
                }
        }

        public bool isActiveLevel(int _level)
        {
            switch (_level)
            {
                case 0: return bossRemain >= colour[((int)Level.First)].remainHp;
                case 1: return bossRemain >= colour[((int)Level.Second)].remainHp && bossRemain < colour[((int)Level.First)].remainHp;
                case 2: return bossRemain >= colour[((int)Level.Third)].remainHp && bossRemain < colour[((int)Level.Second)].remainHp;
                case 3: return bossRemain >= colour[((int)Level.Fourth)].remainHp && bossRemain < colour[((int)Level.Third)].remainHp;
                case 4: return bossRemain >= colour[((int)Level.Fifth)].remainHp && bossRemain < colour[((int)Level.Fourth)].remainHp;
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
