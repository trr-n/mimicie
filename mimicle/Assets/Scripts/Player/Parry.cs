using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class Parry : MonoBehaviour
    {
        [SerializeField]
        GameObject parry;

        float cooltime = 2;
        float duration = 0.5f;
        Stopwatch cooltimer = new(true), durationTimer = new();
        public int Timer => cooltimer.s;
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
            if (cooltimer.sf >= cooltime && Mynput.Down(Values.Key.Parry))
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
        }
    }
}
