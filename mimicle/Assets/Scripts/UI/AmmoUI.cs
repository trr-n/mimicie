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

        void Start()
        {
            player ??= GameObject.FindGameObjectWithTag(constant.Player).GetComponent<Player>();
        }

        void Update()
        {
            // ammoT.text = $"Remain: {ammo.Remain.newline()}Max: {ammo.Max.newline()}Time: {player.Reloading * Time.deltaTime}";
            debugT.text = $"fillamount:{ammoI.fillAmount.newline()}remain:{ammo.Remain.newline()}max:{ammo.Max}";
            if (player.IsReloading)
            {
                StartCoroutine(reload(player.Time2Reload));
            }
            else
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
                ammoI.fillAmount = Mathf.Lerp(ammo.Remain, ammo.Max, timer / time);
                timer += Time.deltaTime;
            }
        }
    }
}
