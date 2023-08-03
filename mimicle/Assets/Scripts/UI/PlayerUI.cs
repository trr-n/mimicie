using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        HP hp_player;

        [SerializeField]
        Image gauge;

        [SerializeField]
        Sprite[] bars;

        void Update()
        {
            gauge.fillAmount = hp_player.Ratio;
        }
    }
}