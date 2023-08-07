using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class PlayersUI : MonoBehaviour
    {
        [SerializeField]
        WaveData wave;

        #region ammo
        [Header("ammo")]
        [SerializeField]
        Image ammoGauge;

        [SerializeField]
        Image ammoSymbol;

        [SerializeField]
        Image ammoStatus;

        [SerializeField]
        Text ammoCurrentT;

        [SerializeField]
        Text ammoMaxT;

        [SerializeField]
        Ammo ammo;

        RectTransform ammoGaugeRT;

        float timerf;
        #endregion

        #region parry
        [Header("parry")]
        [SerializeField]
        Image parryGauge;

        [SerializeField]
        Image parrySymbol;

        [SerializeField]
        Image parryStatus;

        [SerializeField]
        Text parryCurrentT;

        [SerializeField]
        Text parryMaxT;

        [SerializeField]
        Parry parry;

        readonly (Color active, Color inactive) parrySymbolColor = (Color.white, Color.gray);
        #endregion

        #region hp
        [Header("hp")]
        [SerializeField]
        Image hpGauge;

        [SerializeField]
        HP playerHP;

        [SerializeField]
        Text hpMaxT;

        [SerializeField]
        Text hpCurrentT;
        #endregion

        RectTransform parryGaugeRT;

        GameObject playerObj;
        Player player;

        (Vector3 Bullet, Vector3 Parry) GaugeOffset => (-1 * Coordinate.Y, -0.5f * Coordinate.Y);

        void Start()
        {
            ammoCurrentT.SetText(ammo.Max);
            ammoMaxT.SetText("/ " + ammo.Max);

            parryCurrentT.SetText(parry.Now);
            parryMaxT.SetText("/ " + parry.Now);

            hpCurrentT.SetText(playerHP.Max);
            hpMaxT.SetText("/ " + playerHP.Max);

            ammoGaugeRT = ammoGauge.GetComponent<RectTransform>();
            parryGaugeRT = parryGauge.GetComponent<RectTransform>();

            playerObj = Gobject.Find(Constant.Player);
            player = playerObj.GetComponent<Player>();
        }

        void Update()
        {
            GaugePosition();

            AmmoGauge();
            ParryGauge();
            HPGauge();
        }

        void HPGauge()
        {
            float t = playerHP.Ratio * playerHP.Max;
            hpCurrentT.SetText(t);

            hpGauge.fillAmount = playerHP.Ratio;

            float hue = playerHP.Ratio / 360 * 100;
            hpGauge.color = Color.HSVToRGB(hue, 1, 1);
        }

        void GaugePosition()
        {
            Vector3 ammo = playerObj.transform.position + GaugeOffset.Bullet;
            ammoGaugeRT.transform.position = ammo;

            parryGaugeRT.transform.position = ammo + GaugeOffset.Parry;
        }

        void ParryGauge()
        {
            parryStatus.enabled = parry.Disable;

            parrySymbol.color = parry.Disable ? parrySymbolColor.inactive : parrySymbolColor.active;

            parryCurrentT.SetText(parry.Timer);
            parryGauge.fillAmount = parry.Timer / parry.CTs[wave.CurrentActive];

            if (parry.IsParry)
            {
                parryGauge.color = Color.red;
                return;
            }

            float ol = Mathf.Sin(Time.time * 10) / 2 + 0.5f;
            float hue = (ol * 20 + 200) / 360;
            parryGauge.color = Color.HSVToRGB(hue, 1, 1);
        }

        void AmmoGauge()
        {
            ammoSymbol.color = Color.HSVToRGB(Time.unscaledTime, 1, 1);

            ammoStatus.enabled = ammo.IsZero || player.IsReloading;

            int ammoCount = Numeric.Cutail(ammoGauge.fillAmount * ammo.Max);
            ammoCurrentT.SetText(ammoCount);

            float hue = ammo.Ratio / 360 * 100;
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
