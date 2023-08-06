using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class CircleUI : MonoBehaviour
    {
        [SerializeField]
        Text timeT;

        void Update()
        {
            timeT.text = Score.CurrentTime.ToString();
        }
    }
}