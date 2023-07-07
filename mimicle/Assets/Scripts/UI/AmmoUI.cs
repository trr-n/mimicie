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
        Player player;
        [SerializeField]
        Text remainT;
        [SerializeField]
        Text timeT;

        void Update()
        {
            remainT.text = ammo.Remain.ToString();
            timeT.text = (player.IsReloading ?
                Numeric.Round(player.Time2Reload - player.Reload__, 2) :
                player.Time2Reload).ToString();
        }
    }
}
