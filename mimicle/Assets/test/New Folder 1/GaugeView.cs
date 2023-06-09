using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class GaugeView : MonoBehaviour
    {
        int currentValue;
        int maxValue;

        [SerializeField]
        Image image;

        [Serializable]
        struct ColorChange
        {
            public float border;
            public Color color;
        }
        [SerializeField]
        ColorChange[] colorChanges;

        void DoColorChange()
        {
            foreach (var _ in colorChanges)
            {
                if (image.fillAmount <= _.border)
                {
                    image.color = _.color;
                    break;
                }
            }
        }

        public void SetCurrentValue(int value)
        {
            currentValue = value;
            image.fillAmount = (float)currentValue / maxValue;
            DoColorChange();
        }

        public void SetMaxValue(int value)
        {
            maxValue = value;
            image.fillAmount = (float)currentValue / maxValue;
            DoColorChange();
        }
    }
}
