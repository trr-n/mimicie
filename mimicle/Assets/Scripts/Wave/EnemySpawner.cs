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

        public bool Spawnable { get; set; }

        const int FirstWave = 0, SecondWave = 1, ThirdWave = 2;

        const int X = 15;

        Transform playerTransform;

        const int BreakTime = 3;

        float spawnY = -3f;

        int spawnCount = 0;

        List<GameObject> spawnedChargers;

        void Start()
        {
            ActivateWave(FirstWave);

            wave.Set(FirstWave);

            Spawnable = true;

            // StartCoroutine(TestWave1());

            playerTransform = gobject.Find(Const.Player).transform;
        }

        void Update()
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
            qset[FirstWave].timer += Time.deltaTime;

            if (!inProgress(FirstWave))
                return;

            "Wave1".show();

            transform.position = new(X, playerTransform.position.y);

            wave.Set(FirstWave);

            if (qset[FirstWave].timer >= qset[FirstWave].span && slain.Count <= qset[FirstWave].quota)
            {
                var charger = enemies[FirstWave].Instance(transform.position, Quaternion.identity);

                spawnedChargers.Add(charger);

                qset[FirstWave].timer = 0;
            }

            // Chargerが存在してたら return
            foreach (var i in spawnedChargers)
                if (i.gameObject)
                    return;

            // TODO リストの中身が全部 null で且つノルマ以上倒したら次のウェーブへ
            if (slain.Count >= qset[FirstWave].quota)
            {
                "active 2".show();

                ActivateWave(SecondWave);

                slain.ResetCount();
            }
        }

        /// <summary>
        /// ちょっと賢いヤツ(Lil Clever)を出す
        /// </summary>
        void Wave2()
        {
            if (!inProgress(SecondWave))
                return;

            qset[SecondWave].timer += Time.deltaTime;

            "Wave2".show();

            transform.position = new(X, transform.position.y);

            wave.Set(SecondWave);

            // 0123 = 4
            if (qset[SecondWave].timer >= qset[SecondWave].span && spawnCount <= 3)
            {
                enemies[SecondWave].Instance(new(X, spawnY), Quaternion.identity);

                qset[SecondWave].timer = 0;

                spawnCount++;

                spawnY += 8 / 3;
            }

            if (slain.Count >= qset[SecondWave].quota)
            {
                ActivateWave(ThirdWave);

                slain.ResetCount();
            }
        }

        void Wave3()
        {
            qset[ThirdWave].timer += Time.deltaTime;

            if (!inProgress(ThirdWave))
                return;

            "Wave3".show();

            wave.Set(ThirdWave);

            if (qset[ThirdWave].timer >= qset[ThirdWave].quota)
            {
                // ボス、LilC, Chargerを出す

                qset[ThirdWave].timer = FirstWave;
            }

            if (slain.Count >= qset[ThirdWave].quota)
            {
                // ボス討伐後
            }
        }

        bool inProgress(int wave)
        {
            switch (wave)
            {
                case 0:
                    return qset[FirstWave].inProgress && !qset[SecondWave].inProgress && !qset[ThirdWave].inProgress;

                case 1:
                    return !qset[FirstWave].inProgress && qset[SecondWave].inProgress && !qset[ThirdWave].inProgress;

                case 2:
                    return !qset[FirstWave].inProgress && !qset[SecondWave].inProgress && qset[ThirdWave].inProgress;

                default:
                    throw new System.Exception();
            }
        }

        void ActivateWave(int wave)
        {
            switch (wave)
            {
                case 0:
                    qset[FirstWave].inProgress = true;

                    qset[SecondWave].inProgress = false;

                    qset[ThirdWave].inProgress = false;

                    break;

                case 1:
                    qset[FirstWave].inProgress = false;

                    qset[SecondWave].inProgress = true;

                    qset[ThirdWave].inProgress = false;

                    break;

                case 2:
                    qset[FirstWave].inProgress = false;

                    qset[SecondWave].inProgress = false;

                    qset[ThirdWave].inProgress = true;

                    break;
            }
        }

        [System.Obsolete]
        IEnumerator TestWave1()
        {
            wave.Set(SecondWave);

            while (inProgress(FirstWave))
            {
                yield return new WaitForSeconds(qset[FirstWave].span);

                enemies[FirstWave].Instance(transform.position, Quaternion.identity);

                if (slain.Count >= qset[FirstWave].quota)
                {
                    ActivateWave(SecondWave);

                    slain.ResetCount();

                    yield break;
                }
            }
        }
    }
}
