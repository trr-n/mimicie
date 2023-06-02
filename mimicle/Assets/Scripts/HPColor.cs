using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class HPColor : MonoBehaviour
    {
        [SerializeField]
        Image hpBar;

        [SerializeField]
        HP playerHp;

        void Update()
        {
            hpBar.fillAmount = playerHp.Ratio;
        }
    }
}
