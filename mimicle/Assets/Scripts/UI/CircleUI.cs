using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
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

        /// <summary>
        /// ゲージ増加用タイマー
        /// </summary>
        float timerf = 0f;

        void Update()
        {
            UpdateAmmoGauge();

            hpGauge.fillAmount = playerHp.Ratio;
            parryGauge.fillAmount = parry.Timer / 2;
            timeT.text = Score.CurrentTime.ToString();
        }

        void UpdateAmmoGauge()
        {
            var hue = ammo.Ratio / 360 * 100;
            ammoGauge.color = Color.HSVToRGB(hue, 1, 1);

            if (!player.IsReloading)
            {
                ammoGauge.fillAmount = ammo.Ratio;
                timerf = 0f;
                return;
            }

            timerf += Time.deltaTime;
            if (timerf <= player.Time2Reload)
            {
                ammoGauge.fillAmount = Mathf.Lerp(player.PreReloadRatio, 1, timerf / player.Time2Reload);
            }
        }
    }
}