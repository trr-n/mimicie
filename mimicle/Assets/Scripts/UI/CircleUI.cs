using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class CircleUI : MonoBehaviour
    {
        [SerializeField]
        Text ammoT;
        [SerializeField]
        Image hpGauge, parryGauge, ammoGauge;
        [SerializeField]
        Ammo ammo;
        [SerializeField]
        HP playerHp;
        [SerializeField]
        Parry parry;
        [SerializeField]
        Player player;

        // bool boo = false;
        // int remain = 0;

        void Update()
        {
            hpGauge.fillAmount = playerHp.Ratio;
            parryGauge.fillAmount = parry.Timer / 2;
            AMMO();
        }

        void AMMO()
        {
            if (!player.IsReloading)
            {
                ammoT.text = ammo.Remain.ToString();
                ammoGauge.fillAmount = ammo.Ratio;
            }
        }

        public IEnumerator UpdateAmmoGauge(float start, float time)
        {
            Stopwatch _timer = new(true);
            while (_timer.sf <= time)
            {
                yield return null;
                ammoGauge.fillAmount = Mathf.Lerp(start, ammo.Max, _timer.sf / time);
                if (ammoGauge.fillAmount >= 1)
                {
                    ammoGauge.fillAmount = 1;
                    break;
                }
            }
        }
    }
}