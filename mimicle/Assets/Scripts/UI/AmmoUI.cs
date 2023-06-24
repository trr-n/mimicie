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

        bool boolean = false;

        void Start()
        {
            player ??= GameObject.FindGameObjectWithTag(Constant.Player).GetComponent<Player>();
        }

        void Update()
        {
            debugT.text = $"fillamount:{ammoI.fillAmount.newline()}remain:{ammo.Remain.newline()}max:{ammo.Max}";
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
        }

        IEnumerator reload(float time)
        {
            var sw = new Stopwatch(true);
            while (sw.SecondF() < time)
            {
                yield return null;
                ammoI.fillAmount = Mathf.Lerp(ammo.Remain / 10, 1, sw.SpentF(SWFormat.S) / time);
                if (ammoI.fillAmount >= 1)
                    boolean = false;
            }
        }
    }
}
