using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField]
        Ammo ammo;
        [SerializeField]
        Text ammoT;
        [SerializeField]
        Image ammoI;
        [SerializeField]
        Text debugT;
        [SerializeField]
        Player player;
        [SerializeField]
        Image alertT;

        bool boolean = false;

        void Update()
        {
#if UNITY_EDITOR
            debugT.text = $"fillamount:{ammoI.fillAmount}\nremain:{ammo.Remain}\nmax:{ammo.Max}";
#endif
            if (player.IsReloading)
            {
                boolean = true;
                StartCoroutine(reload(player.Time2Reload));
            }
            else if (!(player.IsReloading && boolean))
            {
                ammoI.fillAmount = ammo.Ratio;
                ammoT.text = ammo.Ratio.ToString();
            }
            Alert();
        }

        float alpha = .1f;
        bool isFading = false;
        void Alert()
        {
            alertT.fillAmount = 0.33f;
            var c = alertT.color;
            // if (isFading)
            //     alpha -= Time.deltaTime;
            alpha = alpha <= 0 ? Mathf.Sin(Time.time) - 1 : Mathf.Sin(Time.time);
            alertT.color = new(c.r, c.g, c.b, alpha);
        }

        IEnumerator reload(float time)
        {
            var sw = new Stopwatch(true);
            while (sw.sf <= time && boolean)
            {
                yield return null;
                ammoI.fillAmount = Mathf.Lerp(ammo.Remain / 10, 1, sw.sf / time);
                if (ammoI.fillAmount >= 1)
                    boolean = false;
            }
        }
    }
}
