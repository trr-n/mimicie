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

        readonly (Quaternion Rotation, Vector3 Position, Vector3 Scale) initial = (Quaternion.Euler(0, 0, 90), new(7.75f, 0, 1), new(3, 3, 3));
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
        float spawnSpideSpan = 30;
        const float barrageRapid = 0.2f;
        (int bullets, float range) W1Rapid = (5, 1.25f);
        float speed = 1;
        float baseSpeed = 30f;

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
            transform.setr(initial.Rotation);
            transform.sets(initial.Scale);
            spideSW.Start();
        }

        void OnEnable()
        {
            transform.DOMove(initial.Position, posLerpSpeed).SetEase(Ease.OutCubic);
        }

        void Update()
        {
            Both();
            if (bossHp.IsZero)
                Section.Load(Constant.Final);
        }

        void Both()
        {
            if (!Coordinate.Twins(transform.position, initial.Position))
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
        }

        bool once = false;
        Stopwatch l1sw = new();
        /// <summary>
        /// 75 ~ 100, blue: 5% not homing, fire every second 
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
                yield return new WaitForSeconds(W1Rapid.range * 1.5f);
                for (var i = 0; i < W1Rapid.bullets; i++)
                {
                    bullets[0].Instance(point.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(W1Rapid.range / W1Rapid.bullets);
                }
            }
        }

        One _ = new One();
        /// <summary>
        /// barrage
        /// </summary>
        void Lv2()
        {
            if (!isActiveLevel(((int)Level.Second)))
                return;
            activeLevel = 1;
            _.Once(() =>
            {
                point.transform.eulerAngles = new(0, 0, 60);
                StartCoroutine(Barrage());
            });
            (float Max, float Min) barrageRange = (120, 60);
            if (point.transform.eulerAngles.z > barrageRange.Max || point.transform.eulerAngles.z < barrageRange.Min)
                speed *= -1; // 逆回転
            point.transform.Rotate(new Vector3(0, 0, baseSpeed * speed * Time.deltaTime));
        }
        IEnumerator Barrage()
        {
            int i = 0;
            while (i <= 100) // 100 bullets
            {
                yield return new WaitForSecondsRealtime(barrageRapid);
                i++;
                bullets[1].Instance(point.transform.position, Quaternion.Euler(0, 0, point.transform.eulerAngles.z - 90));
            }
        }

        /// <summary>
        ///TODO 30 ~ 50, yellow: 9% homing
        /// </summary>
        void Lv3()
        {
            if (!isActiveLevel(((int)Level.Third)))
                return;
            activeLevel = 2;
        }

        /// <summary>
        ///TODO 10 ~ 30, orange: 13% homing
        /// </summary>
        void Lv4()
        {
            if (!isActiveLevel(((int)Level.Fourth)))
                return;
            activeLevel = 3;
        }

        bool bb = true;
        /// <summary>
        ///TODO 00 ~ 10, red: 15% homing
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
            if (!(spideSW.SecondF() >= spawnSpideSpan))
                return;
            var spide = mobs[((int)Mobs.Spide)].Instance();
            if (spide.TryGetComponent<Spide>(out var _spide))
            {
                _spide.SetLevel(Rnd.Pro(new Dictionary<int, float>() { { 0, 50 }, { 1, 25 }, { 2, 12.5f } }));
            }
            spideSW.Restart();
            spawnSpideSpan = Rnd.randint(20, 30);
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
