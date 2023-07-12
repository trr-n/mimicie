using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimicle.Extend;

namespace Mimicle
{
    public class DieMenu : MonoBehaviour
    {
        [SerializeField]
        BlurUI blur;

        CanvasGroup canvas;
        float a = 0f;

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        void Update()
        {
            if (!blur.Max)
            {
                return;
            }
            a = Mathf.Clamp(a, 0, 1);
            a += Time.unscaledDeltaTime * 10;
            canvas.alpha = a;
        }
    }
}