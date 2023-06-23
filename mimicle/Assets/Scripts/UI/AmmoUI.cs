using System.Collections;
using System.Collections.Generic;
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
            // ammoT.text = $"Remain: {ammo.Remain.newline()}Max: {ammo.Max.newline()}Time: {player.Reloading * Time.deltaTime}";
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
            var timer = 0f;
            while (timer < time)
            {
                yield return null;
                ammoI.fillAmount = Numeric.Round(Mathf.Lerp(ammo.Remain / 10, 1, timer / time), 1);
                timer += Time.deltaTime;
                if (ammoI.fillAmount >= 1)
                    boolean = false;
            }
        }
    }
}
