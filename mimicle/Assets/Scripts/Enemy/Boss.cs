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
        [SerializeField]
        GameObject[] bullets;

        // [SerializeField]
        // GameObject[] mobs;

        [SerializeField]
        GameObject point;

        [SerializeField]
        BossUI bossUI;

        [SerializeField]
        EnemySpawner spawner;

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
            First = 0,
            Second,
            Third,
            Fourth,
            Fifth
        }

        float posLerpSpeed = 5;

        new PolygonCollider2D collider;
        bool onceCollider = true;

        SpriteRenderer sr;

        HP selfHp, playerHp;
        int selfRemain = 0, playerRemain = 0;

        int activeLevel = 0;
        public int ActiveLevel => activeLevel;

        bool setPos = false;
        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;

        void Start()
        {
            collider = GetComponent<PolygonCollider2D>();
            collider.isTrigger = true;

            sr = GetComponent<SpriteRenderer>();
            sr.color = colour[((int)Level.First)].color;

            playerHp = gobject.Find(constant.Player).GetComponent<HP>();
            selfHp = GetComponent<HP>();
            selfHp.SetMax();
            // base.Start(selfHp);

            transform.setr(Init.rotation);
            transform.sets(Init.scale);
        }

        bool a = false;
        void Update()
        {
            if (spawner.StartWave3 && !a)
            {
                transform.DOMove(Init.position, posLerpSpeed).SetEase(Ease.OutCubic);
                a = true;
            }
            Both();

            if (selfHp.IsZero)
            {
                scene.Load("Final");
            }
        }

        /*
        TODO 残り体力によって攻撃を変える
        75 ~ 100, 青: 5%弾, 毎秒発射
        50 ~ 75, 緑:  7%ホーミング弾
        30 ~ 50, 黄:  9%ホーミング弾
        10 ~ 30, 橙:  13%ホーミング弾
        00 ~ 10, 赤:  13%ホーミング弾
        */
        void Both()
        {
            if (!setPos)
            {
                setPos = transform.position.AlmostSame(Init.position);
                return;
            }
            startBossBattle = true;

            if (onceCollider)
            {
                collider.isTrigger = false;
                onceCollider = false;
            }

            selfRemain = numeric.Percent(selfHp.Ratio);
            playerRemain = numeric.Percent(playerHp.Ratio);

            Lv1();
            Lv2();
            Lv3();
            Lv4();
            Lv5();

            print($"boss: {selfRemain}%, player: {playerRemain}%");
        }

        bool l1 = false;
        void Lv1()
        {
            if (!isActiveLevel(((int)Level.First)))
            {
                return;
            }
            "0".show();
            activeLevel = 0;

            if (!l1)
            {
                StartCoroutine(Lv01(1f));
                l1 = true;
            }
        }

        IEnumerator Lv01(float span)
        {
            bool during = true;
            while (during)
            {
                yield return new WaitForSeconds(span);

                bullets[0].Instance(point.transform.position, Quaternion.identity);
            }
        }

        void Lv2()
        {
            if (!isActiveLevel(1))
            {
                return;
            }
            "1".show();
            activeLevel = 1;
        }

        void Lv3()
        {
            if (!isActiveLevel(2))
            {
                return;
            }
            "2".show();
            activeLevel = 2;
        }

        void Lv4()
        {
            if (!isActiveLevel(3))
            {
                return;
            }
            "3".show();
            activeLevel = 3;
        }

        void Lv5()
        {
            if (!isActiveLevel(4))
            {
                return;
            }
            "4".show();
            activeLevel = 4;
        }

        public void ChangeColor()
        {
            foreach (var i in colour)
            {
                if (selfRemain >= i.remainHp)
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
                    activeLevel = ((int)Level.First);
                    return selfRemain >= colour[((int)Level.First)].remainHp;

                case 1:
                    activeLevel = ((int)Level.Second);
                    return selfRemain >= colour[((int)Level.Second)].remainHp && selfRemain < colour[((int)Level.First)].remainHp;

                case 2:
                    activeLevel = ((int)Level.Third);
                    return selfRemain >= colour[((int)Level.Third)].remainHp && selfRemain < colour[((int)Level.Second)].remainHp;

                case 3:
                    activeLevel = ((int)Level.Fourth);
                    return selfRemain >= colour[((int)Level.Fourth)].remainHp && selfRemain < colour[((int)Level.Third)].remainHp;

                case 4:
                    activeLevel = ((int)Level.Fifth);
                    return selfRemain >= colour[((int)Level.Fifth)].remainHp && selfRemain < colour[((int)Level.Fourth)].remainHp;

                default:
                    throw new System.Exception();
            }
        }

        void OnCollisionEnter2D(Collision2D info)
        {
            if (info.Compare(constant.Bullet))
            {
                ChangeColor();
                bossUI.UpdateBossUI();
                selfHp.Damage(Values.Damage.Player / 2);
            }
        }
    }
}
