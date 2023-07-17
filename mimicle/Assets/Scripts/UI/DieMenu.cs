using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;

namespace Feather
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
            // ブラーがマックスになったらスコア表示
            if (!blur.Max)
            {
                return;
            }

            a = Mathf.Clamp(a, 0, 1);
            a += Time.unscaledDeltaTime * 10;
            canvas.alpha = a;

            if (a >= 1)
            {
                blur.Reblur();
            }
        }
    }
}