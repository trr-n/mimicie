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

        public bool Spawnable { get; set; }

        static class WAVE
        {
            public static int FIRST = 0;

            public static int SECOND = 1;

            public static int THIRD = 2;
        }

        void Start()
        {
            ActivateWave(0);

            wave.Set(0);

            Spawnable = true;

            // StartCoroutine(TestWave1());
        }

        void Update()
        {
            Waves();
        }

        void Waves()
        {
            Wave1();
            Wave2();
            Wave3();
        }

        /// <summary>
        /// チャージャーを一定間隔で出す
        /// </summary>
        void Wave1()
        {
            qset[WAVE.FIRST].timer += Time.deltaTime;

            if (!inProgress(WAVE.FIRST))
                return;

            transform.position = new(15, Mathf.Sin(Time.time));

            wave.Set(WAVE.FIRST);

            if (qset[WAVE.FIRST].timer >= qset[WAVE.FIRST].span)
            {
                enemies[WAVE.FIRST].Instance(transform.position, Quaternion.identity);

                qset[WAVE.FIRST].timer = 0;
            }

            if (slain.Count >= qset[WAVE.FIRST].quota)
            {
                ActivateWave(WAVE.SECOND);

                slain.ResetCount();
            }
        }

        /// <summary>
        /// ちょっと賢いヤツ(Lil Clever)を出す
        /// </summary>
        void Wave2()
        {
            qset[WAVE.SECOND].timer += Time.deltaTime;

            if (!inProgress(WAVE.SECOND))
                return;

            wave.Set(WAVE.SECOND);

            if (qset[WAVE.SECOND].timer >= qset[WAVE.SECOND].span)
            {
                enemies[WAVE.SECOND].Instance(transform.position, Quaternion.identity);

                qset[WAVE.SECOND].timer = 0;
            }

            if (slain.Count >= qset[WAVE.SECOND].quota)
            {
                ActivateWave(WAVE.THIRD);

                slain.ResetCount();
            }
        }

        void Wave3()
        {
            qset[WAVE.THIRD].timer += Time.deltaTime;

            if (!inProgress(WAVE.THIRD))
                return;

            wave.Set(WAVE.THIRD);

            if (qset[WAVE.THIRD].timer >= qset[WAVE.THIRD].quota)
            {
                // ボス、LilC, Chargerを出す

                qset[WAVE.THIRD].timer = WAVE.FIRST;
            }

            if (slain.Count >= qset[WAVE.THIRD].quota)
            {
                // ボス討伐後
            }
        }

        bool inProgress(int wave)
        {
            switch (wave)
            {
                case 0:
                    return qset[WAVE.FIRST].inProgress && !qset[WAVE.SECOND].inProgress && !qset[WAVE.THIRD].inProgress;

                case 1:
                    return !qset[WAVE.FIRST].inProgress && qset[WAVE.SECOND].inProgress && !qset[WAVE.THIRD].inProgress;

                case 2:
                    return !qset[WAVE.FIRST].inProgress && !qset[WAVE.SECOND].inProgress && qset[WAVE.THIRD].inProgress;

                default:
                    throw new System.Exception();
            }
        }

        void ActivateWave(int wave)
        {
            switch (wave)
            {
                case 0:
                    // "Wv.secondst active".show();
                    qset[WAVE.FIRST].inProgress = true;

                    qset[WAVE.SECOND].inProgress = false;

                    qset[WAVE.THIRD].inProgress = false;

                    break;

                case 1:
                    // "2nd active".show();
                    qset[WAVE.FIRST].inProgress = false;

                    qset[WAVE.SECOND].inProgress = true;

                    qset[WAVE.THIRD].inProgress = false;

                    break;

                case 2:
                    // "3rd active".show();
                    qset[WAVE.FIRST].inProgress = false;

                    qset[WAVE.SECOND].inProgress = false;

                    qset[WAVE.THIRD].inProgress = true;

                    break;
            }
        }

        [System.Obsolete]
        IEnumerator Test00()
        {
            wave.Set(WAVE.SECOND);

            while (inProgress(WAVE.FIRST))
            {
                yield return new WaitForSeconds(qset[WAVE.FIRST].span);

                enemies[WAVE.FIRST].Instance(transform.position, Quaternion.identity);

                if (slain.Count >= qset[WAVE.FIRST].quota)
                {
                    ActivateWave(WAVE.SECOND);

                    slain.ResetCount();

                    yield break;
                }
            }
        }
    }
}
