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
        public float Timer => Mathf.Clamp(cooltimer.SecondF(), 0, CTs[wdata.CurrentActive]);

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

        [SerializeField]
        UnityEngine.UI.Text debug;

        void Start()
        {
            parryObj.SetActive(false);
        }

        void Update()
        {
            if (debug != null)
            {
                debug.text = "CT: " + CTs[wdata.CurrentActive] + "\nNow: " + Timer;
            }

            MakeParry();
            isParrying = parryObj.IsActive(Active.Self);
        }

        /// <summary>
        /// パリィ生成
        /// </summary>
        void MakeParry()
        {
            if (cooltimer.sf >= CTs[wdata.CurrentActive] && Inputs.Down(Constant.Parry))
            {
                count++;

                cooltimer.Reset();
                parryObj.SetActive(true);
                durationSW.Start();
            }

            if (durationSW.isRunning && durationSW.sf >= Duration)
            {
                parryObj.SetActive(false);
                durationSW.Reset();
                cooltimer.Start();
            }

            if (cooltimer.isRunning && cooltimer.sf >= CTs[wdata.CurrentActive])
            {
                cooltimer.Stop();
            }
        }
    }
}
