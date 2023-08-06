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
        WaveData wdata;

        [Header("Ammo")]
        [SerializeField]
        Image ammoGauge;

        [SerializeField]
        Text ammoCurrentT;

        [SerializeField]
        Text ammoMaxT;

        [SerializeField]
        Ammo ammo;

        RectTransform ammoGaugeRT;

        float timerf;

        [Header("Parry")]
        [SerializeField]
        Image parryGauge;

        [SerializeField]
        Parry parry;

        [Header("Player")]
        [SerializeField]
        Image hpGauge;

        [SerializeField]
        HP playerHP;

        RectTransform parryGaugeRT;

        GameObject playerObj;
        Player player;

        (Vector3 Bullet, Vector3 Parry) Offset => (-1 * Coordinate.X, -0.5f * Coordinate.X);

        void Start()
        {
            ammoMaxT.text = "/ " + ammo.Max;
            ammoCurrentT.text = ammo.Max.ToString();

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
            hpGauge.fillAmount = playerHP.Ratio;

            float hue = playerHP.Ratio / 360 * 100;
            hpGauge.color = Color.HSVToRGB(hue, 1, 1);
        }

        void GaugePosition()
        {
            Vector3 ammo = playerObj.transform.position + Offset.Bullet;
            ammoGaugeRT.transform.position = ammo;

            parryGaugeRT.transform.position = ammo + Offset.Parry;
        }

        void ParryGauge()
        {
            parryGauge.fillAmount = parry.Timer / parry.CTs[wdata.CurrentActive];

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
            int ammoCount = Numeric.Cutail(ammoGauge.fillAmount * ammo.Max);
            ammoCurrentT.text = ammoCount.ToString();

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
