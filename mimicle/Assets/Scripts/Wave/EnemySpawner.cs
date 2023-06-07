using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        Quota[] qset = new Quota[3];

        // public bool Spawnable { get; set; }

        const int X = 15;

        Transform playerTransform;

        const int BreakingTime = 3;

        float breakingTimer = 0;

        float lilcSpawnY = -3.8f;

        int spawnCount = 0;

        List<GameObject> spawnedChargers = new List<GameObject>();

        bool finals = false;

        float finalTime = 0, finalScore = 0;

        void Start()
        {
            ActivateWave(Wave.First);

            wave.Set(Wave.First);

            // Spawnable = true;

            // StartCoroutine(TestWave1());

            playerTransform = gobject.Find(Const.Player).transform;
        }

        void Update()
        {
            Wave1();

            Wave2();

            Wave3();

            // breakingTimer.show();
        }

        /// <summary>
        /// チャージャーを一定間隔で出す
        /// </summary>
        void Wave1()
        {
            qset[Wave.First].timer += Time.deltaTime;

            if (!inProgress(Wave.First))
            {
                return;
            }

            "Wave1".show();

            transform.position = new(X, playerTransform.position.y);

            wave.Set(Wave.First);

            if (qset[Wave.First].timer >= qset[Wave.First].span && slain.Count <= qset[Wave.First].quota)
            {
                spawnedChargers.Add(enemies[Wave.First].Instance(transform.position, Quaternion.identity));

                qset[Wave.First].timer = 0;
            }

            foreach (var i in spawnedChargers)
            {
                if (i.gameObject)
                {
                    return;
                }
            }

            if (slain.Count >= qset[Wave.First].quota)
            {
                breakingTimer += Time.deltaTime;

                if (breakingTimer >= BreakingTime)
                {
                    "active 2".show();

                    ActivateWave(Wave.Second);

                    slain.ResetCount();

                    breakingTimer = 0;
                }
            }
        }

        /// <summary>
        /// ちょっと賢いヤツ(Lil Clever)を出す
        /// </summary>
        void Wave2()
        {
            if (!inProgress(Wave.Second))
            {
                return;
            }

            qset[Wave.Second].timer += Time.deltaTime;

            "Wave2".show();

            transform.position = new(X, transform.position.y);

            wave.Set(Wave.Second);

            // 0123 = 4
            if (qset[Wave.Second].timer >= qset[Wave.Second].span && spawnCount <= 3)
            {
                enemies[Wave.Second].Instance(new(X, lilcSpawnY), Quaternion.identity);

                qset[Wave.Second].timer = 0;

                spawnCount++;

                lilcSpawnY += 8 / 3f;
            }

            if (slain.Count >= qset[Wave.Second].quota)
            {
                breakingTimer += Time.deltaTime;

                if (breakingTimer >= BreakingTime)
                {
                    ActivateWave(Wave.Third);

                    slain.ResetCount();

                    breakingTimer = 0;
                }
            }
        }

        void Wave3()
        {
            qset[Wave.Third].timer += Time.deltaTime;

            if (!inProgress(Wave.Third))
            {
                return;
            }

            "Wave3".show();

            wave.Set(Wave.Third);

            if (qset[Wave.Third].timer >= qset[Wave.Third].quota)
            {
                // ボス、LilC, Chargerを出す

                qset[Wave.Third].timer = Wave.First;
            }

            if (slain.Count >= qset[Wave.Third].quota)
            {
                if (!finals)
                {
                    finalTime = Score.Time();

                    finalScore = Score.Final();

                    $"time: {finalTime}, score: {finalScore}".show();

                    finals = true;
                }
            }
        }

        bool inProgress(int wave)
        {
            switch (wave)
            {
                case 0:
                    return qset[Wave.First].inProgress && !qset[Wave.Second].inProgress && !qset[Wave.Third].inProgress;

                case 1:
                    return !qset[Wave.First].inProgress && qset[Wave.Second].inProgress && !qset[Wave.Third].inProgress;

                case 2:
                    return !qset[Wave.First].inProgress && !qset[Wave.Second].inProgress && qset[Wave.Third].inProgress;

                default:
                    throw new System.Exception();
            }
        }

        public void ActivateWave(int wave)
        {
            switch (wave)
            {
                case 0:
                    qset[Wave.First].inProgress = true;

                    qset[Wave.Second].inProgress = false;

                    qset[Wave.Third].inProgress = false;

                    break;

                case 1:
                    qset[Wave.First].inProgress = false;

                    qset[Wave.Second].inProgress = true;

                    qset[Wave.Third].inProgress = false;

                    break;

                case 2:
                    qset[Wave.First].inProgress = false;

                    qset[Wave.Second].inProgress = false;

                    qset[Wave.Third].inProgress = true;

                    break;
            }
        }

        [System.Obsolete]
        IEnumerator TestWave1()
        {
            wave.Set(Wave.Second);

            while (inProgress(Wave.First))
            {
                yield return new WaitForSeconds(qset[Wave.First].span);

                enemies[Wave.First].Instance(transform.position, Quaternion.identity);

                if (slain.Count >= qset[Wave.First].quota)
                {
                    ActivateWave(Wave.Second);

                    slain.ResetCount();

                    yield break;
                }
            }
        }
    }
}
