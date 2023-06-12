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
        Boss boss;

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

        public bool Spawnable { get; set; }

        Transform playerTransform;

        List<GameObject> spawnedChargers = new List<GameObject>();

        const int X = 15;

        float breakingTimer = 0;
        const int BreakingTime = 3;

        int spawnCount = 0;

        float lilcSpawnY = -3.5f;

        static float finalTime = 0, finalScore = 0;
        public static float[] Finals => new float[] { finalTime, finalScore };

        bool finals = false;

        public bool StartWave3 { get; set; }

        void Start()
        {
            ActivateWave(Wave.First);
            wave.Set(Wave.First);

            Spawnable = true;

            playerTransform = gobject.Find(constant.Player).transform;
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
            if (!inProgress(Wave.First))// && !Spawnable)
            {
                return;
            }

            qset[Wave.First].timer += Time.deltaTime;

            "Wave1".show();

            transform.position = new(X, playerTransform.position.y);

            wave.Set(Wave.First);

            if (qset[Wave.First].timer >= qset[Wave.First].span &&
                slain.Count <= qset[Wave.First].quota)
            {
                spawnedChargers.Add(
                    enemies[Wave.First].Instance(transform.position, Quaternion.identity));

                qset[Wave.First].timer = 0;
            }

            foreach (var i in spawnedChargers)
            {
                if (i.IsExist())
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
            if (!inProgress(Wave.Second))// && !Spawnable)
            {
                return;
            }

            wave.Set(Wave.Second);

            qset[Wave.Second].timer += Time.deltaTime;

            "Wave2".show();

            transform.position = new(X, transform.position.y);

            // 0123 = 4
            if (qset[Wave.Second].timer >= qset[Wave.Second].span && spawnCount < 4)
            {
                enemies[Wave.Second].Instance(new(X, lilcSpawnY), Quaternion.identity);
                qset[Wave.Second].timer = 0;

                spawnCount++;
                lilcSpawnY += 8 / 3.4f; //04255319148936f;
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

            if (!inProgress(Wave.Third) && !boss.StartBossBattle) // && !Spawnable)
            {
                return;
            }

            "Wave3".show();
            if (!StartWave3)
            {
                StartWave3 = true;
            }

            wave.Set(Wave.Third);

            if (qset[Wave.Third].timer >= qset[Wave.Third].quota)
            {
                qset[Wave.Third].timer = 0;
            }

            if (slain.Count >= qset[Wave.Third].quota)
            {
                if (!finals)
                {
                    finalTime = Score.Time();
                    finalScore = Score.Final();

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
