using UnityEngine;
using Self.Utility;

namespace Self
{
    public class Parry : MonoBehaviour
    {
        [SerializeField]
        GameObject parryObj;

        /// <summary>
        /// クールタイム
        /// </summary>
        const float CT = 2;

        /// <summary>
        /// 持続時間
        /// </summary>
        float duration = 0.5f;

        /// <summary>
        /// クールタイム計測用ストップウォッチ
        /// </summary>
        Stopwatch cooltimer = new(true);

        /// <summary>
        /// 持続時間計測用ストップウォッチ
        /// </summary>
        Stopwatch durationTimer = new();

        /// <summary>
        /// クールタイム表示用
        /// </summary>
        public float Timer => Mathf.Clamp(cooltimer.SecondF(1), 0, CT);

        /// <summary>
        /// クールタイム中ならTrue
        /// </summary>
        public bool isRecasting => cooltimer.isRunning;

        bool isParrying = false;
        /// <summary>
        /// パリィ中ならTrue
        /// </summary>
        public bool IsParrying => isParrying;

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
            if (cooltimer.sf >= CT && Feed.Down(Values.Key.Parry))
            {
                cooltimer.Reset();
                parryObj.SetActive(true);
                durationTimer.Start();
            }

            if (durationTimer.isRunning && durationTimer.sf >= duration)
            {
                parryObj.SetActive(false);
                durationTimer.Reset();
                cooltimer.Start();
            }

            if (cooltimer.isRunning && cooltimer.sf >= CT)
            {
                cooltimer.Stop();
            }
        }
    }
}
