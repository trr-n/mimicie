using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using DG.Tweening;
using UnityEngine.UI;

namespace Mimical
{
    public class Boss : MonoBehaviour//Enemy
    {
        [SerializeField, Tooltip("0: 5%\n1:7%\n2:9%\n3:11%\n4:15%")]
        GameObject[] bullets;

        // [SerializeField]
        // GameObject[] mobs;

        [SerializeField]
        GameObject point;

        [SerializeField]
        BossUI bossUI;

        // [SerializeField]
        // EnemySpawner spawner;

        [System.Serializable]
        struct Colour
        {
            public int remainHp;
            public Color color;
        }

        [SerializeField]
        Colour[] colour = new Colour[5];
        // Colour2[] colour = new Colour2[5];

        class Init
        {
            public static Quaternion rotation = Quaternion.Euler(0, 0, 90);
            public static Vector3 position = new(7, 0, 1);
            public static Vector3 scale = Vector3.one * 5;
            // public static Color color = new(0.4f, 0.9764706f, 1);
        }

        enum Level
        {
            First = 0, Second, Third, Fourth, Fifth
        }

        float posLerpSpeed = 5;

        new PolygonCollider2D collider;
        bool onceCollider = true;
        SpriteRenderer sr;
        HP bossHp, playerHp;
        int bossRemain = 0, playerRemain = 0;
        int activeLevel = 0;
        public int ActiveLevel => activeLevel;
        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;

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
            // base.Start(selfHp);

            transform.setr(Init.rotation);
            transform.sets(Init.scale);
        }

        bool a = false;
        void Update()
        {
            // print("active level: " + activeLevel);

            if (/*spawner.StartWave3 && */!a)
            {
                transform.DOMove(Init.position, posLerpSpeed).SetEase(Ease.OutCubic);
                a = true;
            }
            Both();

            if (bossHp.IsZero)
                scene.Load("Final");
        }

        void Both()
        {
            if (!Numeric.AlmostSame(transform.position, Init.position))
                return;
            startBossBattle = true;

            if (onceCollider)
            {
                collider.isTrigger = false;
                onceCollider = false;
            }

            bossRemain = Numeric.Percent(bossHp.Ratio);
            playerRemain = Numeric.Percent(playerHp.Ratio);
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
                yield return new WaitForSeconds(AtRandom.randint(1, 10));
                bullets[0].Instance(point.transform.position, Quaternion.identity);
            }
        }

        /// <summary>
        /// 50 ~ 75, 緑:  7%ホーミング弾
        /// </summary>
        void Lv2()
        {
            if (!isActiveLevel(((int)Level.Second)))
                return;
            activeLevel = 1;
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

        /// <summary>
        /// 00 ~ 10, 赤:  15%ホーミング弾
        /// </summary>
        void Lv5()
        {
            if (!isActiveLevel(((int)Level.Fifth)))
                return;
            activeLevel = 4;
        }

        public void ChangeBodyColor()
        {
            foreach (var i in colour)
            {
                if (bossRemain >= i.remainHp)
                {
                    sr.color = i.color;
                    break;
                }
            }
        }

        bool isActiveLevel(int _level)
        {
            switch (_level)
            {
                case 0:
                    return bossRemain >= colour[((int)Level.First)].remainHp;

                case 1:
                    return bossRemain >= colour[((int)Level.Second)].remainHp && bossRemain < colour[((int)Level.First)].remainHp;

                case 2:
                    return bossRemain >= colour[((int)Level.Third)].remainHp && bossRemain < colour[((int)Level.Second)].remainHp;

                case 3:
                    return bossRemain >= colour[((int)Level.Fourth)].remainHp && bossRemain < colour[((int)Level.Third)].remainHp;

                case 4:
                    return bossRemain >= colour[((int)Level.Fifth)].remainHp && bossRemain < colour[((int)Level.Fourth)].remainHp;

                default:
                    throw new System.Exception();
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
