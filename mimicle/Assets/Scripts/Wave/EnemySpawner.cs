using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        Slain slain;

        [SerializeField]
        Wave wave;

        [SerializeField]
        [Tooltip("0: Charger, 1: LilC, 2: Boss")]
        GameObject[] enemies;

        public bool Spawnable { get; set; }

        [Serializable]
        struct Quota
        {
            public int quota;
            public int span;
            public bool inProgress;
            public float timer;
        }
        [SerializeField]
        [Header("0: Wave1, 1: Wave2, 2: Wave3")]
        Quota[] qset = new Quota[3];

        bool once2 = false, once3 = false;

        void Start()
        {
            ActivateWave(1);

            wave.Set(1);

            Spawnable = true;

            // StartCoroutine(TestWave1());
        }

        void Update()
        {
            transform.position = new(15, Mathf.Sin(Time.time));

            Waves();
        }

        void Waves()
        {
            Wave1();
            Wave2();
            Wave3();
        }

        IEnumerator TestWave1()
        {
            wave.Set(1);

            while (inProgress(1))
            {
                yield return new WaitForSeconds(qset[0].span);

                enemies[0].Instance(transform.position, Quaternion.identity);

                if (slain.Count >= qset[0].quota)
                {
                    ActivateWave(2);
                    // wave.Next();

                    slain.ResetCount();

                    yield break;
                }
            }
        }

        /// <summary>
        /// チャージャーを一定間隔で出す
        /// </summary>
        void Wave1()
        {
            qset[0].timer += Time.deltaTime;

            if (!inProgress(1))
                return;

            wave.Set(1);

            if (qset[0].timer >= qset[0].span)
            {
                enemies[0].Instance(transform.position, Quaternion.identity);

                qset[0].timer = 0;
            }

            if (slain.Count >= qset[0].quota)
            {
                ActivateWave(2);
                // wave.Next();

                slain.ResetCount();
            }
        }

        /// <summary>
        /// ちょっと賢いヤツ(Lil Clever)を2匹出す
        /// </summary>
        void Wave2()
        {
            qset[1].timer += Time.deltaTime;

            if (!inProgress(2))
                return;

            if (qset[1].timer >= qset[1].span)
            {
                wave.Set(2);

                "in wave2".show();

                qset[1].timer = 0;
            }

            if (slain.Count >= qset[1].quota && !once2)
            {
                once2 = true;

                ActivateWave(3);
                // wave.Next();

                slain.ResetCount();

                // "2nd wave is done".show();
            }
        }

        /// <summary>
        /// 体力が多いボスを1体、LilC, Chargerを出し続ける
        /// </summary>
        void Wave3()
        {
            if (!inProgress(3))
                return;

            wave.Set(3);

            "wave3 active".show();

            if (slain.Count >= qset[2].quota)
            {
                "wave3 is done".show();

                // scene.Load();
            }
        }

        bool inProgress(int wave)
        {
            switch (wave)
            {
                case 1:
                    return qset[0].inProgress && !qset[1].inProgress && !qset[2].inProgress;
                case 2:
                    return !qset[0].inProgress && qset[1].inProgress && !qset[2].inProgress;
                case 3:
                    return !qset[0].inProgress && !qset[1].inProgress && qset[2].inProgress;
                default:
                    throw new System.Exception();
            }
        }

        void ActivateWave(int wave)
        {
            switch (wave)
            {
                case 1:
                    // "1st active".show();
                    qset[0].inProgress = true;
                    qset[1].inProgress = false;
                    qset[2].inProgress = false;
                    break;

                case 2:
                    // "2nd active".show();
                    qset[0].inProgress = false;
                    qset[1].inProgress = true;
                    qset[2].inProgress = false;
                    break;

                case 3:
                    // "3rd active".show();
                    qset[0].inProgress = false;
                    qset[1].inProgress = false;
                    qset[2].inProgress = true;
                    break;
            }
        }
    }
}
