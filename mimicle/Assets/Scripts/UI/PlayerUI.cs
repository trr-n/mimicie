using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        HP hp_player;
        [SerializeField]
        Image gauge;
        [SerializeField]
        Image face;
        [SerializeField]
        Sprite[] faces;

        void Update()
        {
            // TODO make player ui
            gauge.fillAmount = hp_player.Ratio;
        }
    }
}