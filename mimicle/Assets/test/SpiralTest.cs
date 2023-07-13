using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnionEngine.Extend;

namespace UnionEngine.Test
{
    public class SpiralTest : MonoBehaviour
    {
        [SerializeField]
        Text xyT;

        [SerializeField]
        float a = 33f, r = 10f, inc = 1f;

        void Update()
        {
            Archimedes(a, r, inc);
        }

        void Archimedes(float a, float r, float inc)
        {
            // x=acosθ/θ, y=asinθ/θ
            float theta0 = transform.eulerAngles.x + inc, cosTheta = Mathf.Cos(Time.time);
            var x = theta0 / a * cosTheta;

            float theta1 = transform.eulerAngles.y + inc, sinTheta = Mathf.Sin(Time.time);
            var y = theta1 / a * sinTheta;

            transform.Translate(new Vector2(x, y) * r * Time.deltaTime);

            // r=a+bθ

            xyT.text = "x: " + x.NewLine() + "y: " + y;
        }
    }
}
