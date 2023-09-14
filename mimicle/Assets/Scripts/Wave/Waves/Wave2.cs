using System.Collections.Generic;
using UnityEngine;
using Self.Utils;
using System.Collections;

namespace Self.Game
{
    public sealed class Wave2 : MonoBehaviour//WaveData
    {
        [SerializeField]
        GameObject[] enemies;

        [SerializeField]
        Slain slain;

        [SerializeField]
        GameObject item;

        /// <summary>
        /// lilcの生成座標
        /// </summary>
        float lilcSpawnY = -3.5f;

        /// <summary>
        /// lilcの生成間隔(時間)
        /// </summary>
        readonly float lilcSpawnSpan = 1f;

        /// <summary>
        /// lilcの生成間隔(座標)
        /// </summary>
        const float Offset = 3.4f;

        /// <summary>
        /// lilcの生成座標
        /// </summary>
        const int X = 15;

        /// <summary>
        /// lilcの生成回数上限
        /// </summary>
        const int Quota = 4;

        /// <summary>
        /// ウェーブ2クリア条件
        /// </summary>
        bool isDone1 => slain.Count >= Quota;

        const float BreakTime = 2f;
        readonly Stopwatch nextSW = new();
        // readonly Stopwatch spanwSW = new(true);
        readonly Runner LilC = new();
        readonly Runner drop = new();

        WaveData data;

        void OnEnable()
        {
            data = transform.parent.gameObject.GetComponent<WaveData>();
        }

        void Update()
        {
            Make();
        }

        void Make()
        {
            if (!data.IsActiveWave(1)) return;

            transform.SetPosition(X);

            LilC.RunOnce(() => StartCoroutine(MakeLilC(lilcSpawnSpan)));

            // ノルマクリアしたら
            if (isDone1)
            {
                // ストップウォッチ開始
                nextSW.Start();

                // 休憩時間が終わったら
                if (nextSW.sf >= BreakTime)
                {
                    // ブキアップグレードアイテムを落とす
                    drop.RunOnce(() => item.Generate());

                    // ウェーブ3をアクティブに
                    data.ActivateWave((int)Activate.Third);

                    // 討伐回数をリセット
                    slain.ResetCount();

                    // nextSWを廃棄
                    nextSW.Rubbish();
                }
            }
        }

        /// <summary>
        /// interval秒おきにlilcを生成する
        /// </summary>
        /// <returns></returns>
        IEnumerator MakeLilC(float interval)
        {
            for (ushort count = 0; count < 4; count++)
            {
                // lilc生成
                enemies[0].Generate(new(X, lilcSpawnY));

                // // 再生成用タイマーリスタート
                // spanwSW.Restart();

                // lilcの生成座標をずらす
                lilcSpawnY += 8 / Offset; //04255319148936f;

                // 生成回数が偶数のとき
                if (count % 2 == 0)
                {
                    // ninjaをランダムな座標に生成する
                    Vector2 ninja = new(x: Rand.Float(-8, 8), y: Rand.Float(-4, 4));
                    enemies[1].Generate(ninja);
                }

                yield return new WaitForSeconds(interval);
            }
        }
    }
}
