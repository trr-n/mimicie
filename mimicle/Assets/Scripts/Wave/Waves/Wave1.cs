using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public sealed class Wave1 : MonoBehaviour
    {
        [SerializeField]
        WaveData data;

        [SerializeField]
        GameObject chargerObj;

        [SerializeField]
        Slain slain;

        [SerializeField]
        GameObject upgradeItem;

        readonly List<GameObject> spawned = new();
        readonly Stopwatch nextWaveSW = new();
        readonly Stopwatch waveSW = new();
        readonly Runner makeChargers = new();

        /// <summary>
        /// chargerの生成時間の上限
        /// </summary>
        readonly float WaveLength = 15f;

        /// <summary>
        /// wave2までの休憩時間
        /// </summary>
        readonly float BreakTime = 2f;

        /// <summary>
        /// chargerの生成回数
        /// </summary>
        int spawnCount = 0;

        /// <summary>
        /// chargerの最大生成回数, 生成間隔(時間), 生成間隔(座標)
        /// </summary>
        (int Count, float Span, float Space) Spawn => (3, 2, 1.5f);

        /// <summary>
        /// chargerの最大生成回数, ノルマ
        /// </summary>
        (int Spawn, int Slain) Quota => (Spawn.Count * 5, Spawn.Count * 3);

        /// <summary>
        /// chargerのX座標
        /// </summary>
        const int X = 15;

        readonly Runner upg = new();

        void OnEnable() => waveSW.Start();

        void Update()
        {
            // ウェーブ1じゃなかったらリターン
            if (!data.IsActiveWave(0)) return;

            // charger生成用のコルーチン実行
            makeChargers.RunOnce(() => { StartCoroutine(Chargers(Spawn.Span / Spawn.Count)); });

            // 制限時間以内、生成回数が上限以下、討伐回数がノルマ以下だったらリターン(charger生成停止)
            if (!(waveSW.sf >= WaveLength && spawnCount >= Quota.Spawn && slain.Count >= Quota.Slain)) return;

            // シーンにchargerがいたらリターン
            foreach (var charger in spawned)
            {
                if (charger) return;
            }

            // charger生成コルーチン停止
            StopCoroutine(Chargers(0));

            // 次のウェーブに移るまでのストップウォッチ開始
            nextWaveSW.Start();

            // ストップウォッチが休憩時間を超えたら
            if (nextWaveSW.SecondF() >= BreakTime)
            {
                // ブキアップグレードアイテムを生成
                upg.RunOnce(() => upgradeItem.Generate(Vector2.zero));

                // 討伐回数リセット
                slain.ResetCount();

                // アクティブウェーブを2に変更
                data.ActivateWave((int)Activate.Second);

                // nextWaveSWを廃棄
                Stopwatch.Rubbish(nextWaveSW);
            }
        }

        /// <summary>
        /// interval秒おきにchargerを生成
        /// </summary>
        IEnumerator Chargers(float interval)
        {
            spawnCount = 0;
            float offset, spawnY;

            while (true)
            {
                yield return new WaitForSeconds(Spawn.Span);

                offset = Spawn.Space;
                Transform playerT = Gobject.GetWithTag<Transform>(Constant.Player);

                for (ushort count = 0; count < Spawn.Count; count++)
                {
                    spawnY = playerT.position.y + offset;

                    spawnY = Mathf.Clamp(spawnY, -4, 4);
                    spawned.Add(chargerObj.Generate(new(X, spawnY), Quaternion.identity));

                    spawnCount++;
                    offset -= Spawn.Space;

                    yield return new WaitForSeconds(interval);
                }
            }
        }
    }
}