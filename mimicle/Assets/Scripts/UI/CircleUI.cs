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
            // timeT.SetText(Numeric.Cutail(Score.CurrentTime));
            timeT.SetText(Score.CurrentTime);
        }
    }
}