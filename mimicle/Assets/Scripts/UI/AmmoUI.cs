using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;
using DG.Tweening;

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
        Player player;

        void Start()
        {
            // player ??= GameObject.FindGameObjectWithTag(constant.Player)
            //     .GetComponent<Player>();
        }

        void Update()
        {
            // ammoT.text = $"Remain: {ammo.Remain.newline()}Max: {ammo.Max.newline()}Time: {player.Reloading * Time.deltaTime}";
            // if (player.IsReloading)
            {
                // ammoI.fillAmount = Mathf.Lerp(
                // ammo.Remain, ammo.Max, player.Reloading * Time.deltaTime);
            }
            // else
            {
                ammoI.fillAmount = ammo.Ratio;
                ammoT.text = ammo.Ratio.ToString();
            }
        }

        IEnumerator reload()
        {
            var timer = 0f;
            while (timer < player.Reloading)
            {
                yield return null;
                Mathf.Lerp(ammo.Remain, ammo.Max, timer / player.Reloading);
                timer += Time.deltaTime;
            }
        }
    }
}
