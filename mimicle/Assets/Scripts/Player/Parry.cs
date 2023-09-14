using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class Parry : MonoBehaviour
    {
        [SerializeField]
        GameObject parryObj;

        [SerializeField]
        WaveData wdata;

        /// <summary>
        /// クールタイム(秒)
        /// </summary>
        // public float CT => 2f;

        public float[] CTs => new float[] { 1.5f, 1.25f, 1f };

        public float Now => CTs[wdata.CurrentActive];

        public bool Disable => !(Timer >= Now);

        /// <summary>
        /// 持続時間
        /// </summary>
        const float Duration = 0.5f;

        /// <summary>
        /// クールタイム計測用ストップウォッチ
        /// </summary>
        readonly Stopwatch cooltimer = new(true);

        /// <summary>
        /// 持続時間計測用ストップウォッチ
        /// </summary>
        readonly Stopwatch durationSW = new();

        /// <summary>
        /// クールタイム表示用
        /// </summary>
        public float Timer => Mathf.Clamp(cooltimer.SecondF(1), 0, CTs[wdata.CurrentActive]);

        /// <summary>
        /// クールタイム中ならTrue
        /// </summary>
        public bool IsRecasting => cooltimer.isRunning;

        bool isParrying = false;
        /// <summary>
        /// パリィ中ならTrue
        /// </summary>
        public bool IsParry => isParrying;

        ushort count = 0;
        public ushort Count => count;

        void Start()
        {
            parryObj.SetActive(false);
        }

        void Update()
        {
            MakeParry();
            isParrying = parryObj.IsActive(Active.Self);
        }

        /// <summary>
        /// パリィ生成
        /// </summary>
        void MakeParry()
        {
            // クールタイムおわってて、パリィボタン押されたら
            if (cooltimer.sf >= CTs[wdata.CurrentActive] && Inputs.Down(Constant.Parry))
            {
                // カウントアーップ！
                count++;

                // タイマーリセット
                cooltimer.Reset();

                // ぱりーの表示
                parryObj.SetActive(true);

                // 持続時間計測用ストップウォッチスタート
                durationSW.Start();
            }

            // 持続中じゃなかったら
            if (durationSW.isRunning && durationSW.sf >= Duration)
            {
                // ぱりー非表示に
                parryObj.SetActive(false);

                // 持続時間計測すとっぷうぉっちリセット
                durationSW.Reset();

                // ぱりぃーークールタイム計測かいし
                cooltimer.Start();
            }

            // クールタイマーランニング中で、上限以上だったら
            if (cooltimer.isRunning && cooltimer.sf >= CTs[wdata.CurrentActive])
            {
                // タイマー停止
                cooltimer.Stop();
            }
        }
    }
}
