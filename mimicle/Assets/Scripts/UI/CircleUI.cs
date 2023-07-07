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
        Image hpGauge, parryGauge, reloadGauge;
        [SerializeField]
        Ammo ammo;
        [SerializeField]
        HP playerHp;
        [SerializeField]
        Parry parry;
        [SerializeField]
        Player player;

        bool boo = false;

        void Update()
        {
            hpGauge.fillAmount = playerHp.Ratio;
            parryGauge.fillAmount = parry.Timer / 2;

            ammoT.text = ammo.Remain.ToString();
            ReloadGauge();
        }

        // TODO
        void ReloadGauge()
        {
            if (!player.IsReloading)
            {
                reloadGauge.fillAmount = player.ReloadProgress;
            }
            if (ammo.Remain > 0 && Mynput.Down(Values.Key.Reload) && !boo)
            {
                boo = true;
            }
            if (!boo)
            {
                print(reloadGauge.fillAmount);
            }
        }
    }
}