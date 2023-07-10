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
        Text timeT;
        [SerializeField]
        Image hpGauge, hpbgGauge, parryGauge, ammoGauge;
        [SerializeField]
        Ammo ammo;
        [SerializeField]
        HP playerHp;
        [SerializeField]
        Parry parry;
        [SerializeField]
        Player player;

        [System.Serializable]
        struct ChangeColors
        {
            public Color color;
            public float border;
        }
        [SerializeField]
        ChangeColors[] change = new ChangeColors[4];
        float timerf = 0f;
        float prehp = 1f;
        Stopwatch delaySW = new();

        void Update()
        {
            UpdateAmmoGauge();
            UpdateAmmoGaugeColor();
            hpGauge.fillAmount = playerHp.Ratio;
            parryGauge.fillAmount = parry.Timer / 2;
            timeT.text = Score.Time.ToString();
        }

        void UpdateAmmoGauge()
        {
            if (!player.IsReloading)
            {
                ammoGauge.fillAmount = ammo.Ratio;
                timerf = 0f;
                return;
            }
            timerf += Time.deltaTime;
            if (timerf <= player.Time2Reload)
            {
                ammoGauge.fillAmount = Mathf.Lerp(player.ratio, 1, timerf / player.Time2Reload);
            }
        }

        void UpdateAmmoGaugeColor()
        {
            foreach (var i in change)
            {
                if (ammoGauge.fillAmount <= i.border)
                {
                    ammoGauge.color = i.color;
                    break;
                }
            }
        }
    }
}