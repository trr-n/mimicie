using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sample
{
    public class HP : MonoBehaviour
    {
        [SerializeField]
        int currentValue;
        public int Current => currentValue;

        [SerializeField]
        int maxValue;
        public int Max => maxValue;

        [SerializeField]
        UnityEvent<int> onCurrentValueChanged;

        [SerializeField]
        UnityEvent<int> onMaxValueChanged;

        void Start()
        {
            currentValue = maxValue;
            onMaxValueChanged?.Invoke(maxValue);
            onCurrentValueChanged?.Invoke(currentValue);
        }

        public void Add(int value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            onCurrentValueChanged?.Invoke(currentValue);
        }
    }
}
