using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Self.Test
{
    public class Deb : MonoBehaviour
    {
        [SerializeField]
        GameObject point;
        [SerializeField]
        Text t;

        void Update()
        {
            t.text = $"world:{point.transform.eulerAngles}\nlocal:{point.transform.localEulerAngles}";
        }
    }
}