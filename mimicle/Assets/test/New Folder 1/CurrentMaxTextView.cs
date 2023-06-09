using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class CurrentMaxTextView : MonoBehaviour
    {
        [SerializeField]
        Text currentValueText;

        [SerializeField]
        Text maxValueText;

        public void SetCurrentValue(int value)
        {
            currentValueText.text = value.ToString();
        }

        public void SetMaxValue(int value)
        {
            maxValueText.text = value.ToString();
        }
    }
}
