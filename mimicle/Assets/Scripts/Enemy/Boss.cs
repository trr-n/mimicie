using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;
using DG.Tweening;
using UnityEngine.UI;

namespace Mimical
{
    public class Boss : Enemy
    {
        [SerializeField]
        GameObject[] bullets;

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

        float posLerpSpeed = 5;

        new PolygonCollider2D collider;
        bool onceCollider = true;

        SpriteRenderer sr;

        HP selfHp, playerHp;
        int selfRemain = 0, playerRemain = 0;

        bool setPos = false;
        bool startBossBattle = false;
        public bool StartBossBattle => startBossBattle;

        void Start()
        {
            collider = GetComponent<PolygonCollider2D>();
            collider.isTrigger = true;

            sr = GetComponent<SpriteRenderer>();
            sr.color = colour[0].color;

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
        ?残り体力によって攻撃を変える
        75 ~ 100, 青:
            n%ダメージの弾, 毎秒発射
        50 ~ 75, 緑:
            5ダメージホーミング弾
        30 ~ 50, 黄色:
            7ダメージホーミング弾
        10 ~ 30, オレンジ:
            9ダメージホーミング弾
        00 ~ 10, 赤:
            11ダメージホーミング弾
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

            // $"boss: {selfRemain}, player: {playerRemain}".show();
        }

        bool l1 = false;
        void Lv1()
        {
            if (!isActiveLevel(0))
            {
                return;
            }
            "0".show();

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
        }

        void Lv3()
        {
            if (!isActiveLevel(2))
            {
                return;
            }
            "2".show();
        }

        void Lv4()
        {
            if (!isActiveLevel(3))
            {
                return;
            }
            "3".show();
        }

        void Lv5()
        {
            if (!isActiveLevel(4))
            {
                return;
            }
            "4".show();
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

        bool isActiveLevel(int level)
        {
            switch (level)
            {
                case 0:
                    return selfRemain >= colour[0].remainHp;

                case 1:
                    return selfRemain >= colour[1].remainHp && selfRemain < colour[0].remainHp;

                case 2:
                    return selfRemain >= colour[2].remainHp && selfRemain < colour[1].remainHp;

                case 3:
                    return selfRemain >= colour[3].remainHp && selfRemain < colour[2].remainHp;

                case 4:
                    return selfRemain >= colour[4].remainHp && selfRemain < colour[3].remainHp;

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

        protected override void Move() {; }
    }
}
