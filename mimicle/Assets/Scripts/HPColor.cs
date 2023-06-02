using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
{
    public class HPColor : MonoBehaviour
    {
        [System.Serializable]
        struct Colors
        {
            public float remain;
            public Color color;
        }
        [SerializeField]
        Colors[] colors;

        [SerializeField]
        Image hpBar;

        [SerializeField]
        HP playerHp;

        void Update()
        {
            hpBar.fillAmount = playerHp.Ratio;
            Changing();
        }

        void Changing()
        {
            foreach (var _ in colors)
            {
                if (_.remain >= playerHp.Ratio)
                {
                    hpBar.color = _.color;
                }
            }
        }
    }
}
