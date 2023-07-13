using UnityEngine;
using UnionEngine.Extend;

namespace UnionEngine
{
    public class Parry : MonoBehaviour
    {
        [SerializeField]
        GameObject parry;

        const float CT = 2;
        float duration = 0.5f;
        Stopwatch cooltimer = new(true), durationTimer = new();
        public float Timer => Mathf.Clamp(cooltimer.SecondF(1), 0, CT);
        public bool isCT => cooltimer.isRunning;
        bool isParry = false;
        public bool IsParry => isParry;

        void Start()
        {
            parry.SetActive(false);
        }

        void Update()
        {
            MakeParry();
            isParry = parry.IsActive(Active.Self);
        }

        void MakeParry()
        {
            if (cooltimer.sf >= CT && Mynput.Down(Values.Key.Parry))
            {
                cooltimer.Reset();
                parry.SetActive(true);
                durationTimer.Start();
            }

            if (durationTimer.isRunning && durationTimer.sf >= duration)
            {
                parry.SetActive(false);
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
