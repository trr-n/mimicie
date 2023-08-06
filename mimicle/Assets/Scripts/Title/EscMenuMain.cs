using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class EscMenuMain : MonoBehaviour
    {
        [SerializeField]
        Material blur;
        [SerializeField]
        GameObject[] buttons;

        CanvasGroup canvas;

        bool isActive = false;
        public bool IsActive => isActive;
        const float speeds = 10;

        enum f { fin, fout }

        const string MatName = "_Blur";

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;

            blur.SetFloat(MatName, 0f);
        }

        void Update()
        {
            if (Inputs.Down(Constant.Menu))
            {
                if (!isActive)
                {
                    StartCoroutine(Fade(f.fin));
                }
                else
                {
                    StartCoroutine(Fade(f.fout));
                }
            }
        }

        IEnumerator Fade(f ff)
        {
            float alpha;

            switch (ff)
            {
                case f.fin:
                    alpha = 0f;
                    while (alpha <= 1)
                    {
                        yield return null;
                        alpha = Mathf.Clamp(alpha, 0, 1);
                        alpha += Time.deltaTime * speeds;
                        blur.SetFloat(MatName, alpha * 10);
                        canvas.alpha = alpha;
                    }
                    isActive = true;
                    break;

                case f.fout:
                    alpha = 1f;
                    while (alpha >= 0)
                    {
                        yield return null;
                        alpha = Mathf.Clamp(alpha, 0, 1);
                        alpha -= Time.deltaTime * speeds;
                        blur.SetFloat(MatName, alpha * 10);
                        canvas.alpha = alpha;
                    }
                    isActive = false;
                    break;
            }
        }
    }
}