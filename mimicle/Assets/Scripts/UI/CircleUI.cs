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
        Image hpGauge, parryGauge, ammoGauge;
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

        void Update()
        {
            hpGauge.fillAmount = playerHp.Ratio;
            parryGauge.fillAmount = parry.Timer / 2;
            timeT.text = Score.Time.ToString();
            if (!player.IsReloading)
            {
                ammoGauge.fillAmount = ammo.Ratio;
            }
            UpdateAmmoGaugeColor();
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