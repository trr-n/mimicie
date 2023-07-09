using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
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

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
            blur.SetFloat("_Blur", 0f);
        }

        void Update()
        {
            if (Mynput.Down(KeyCode.Escape))
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
            float alpha = 0f;
            switch (ff)
            {
                case f.fin:
                    alpha = 0f;
                    while (alpha <= 1)
                    {
                        yield return null;
                        alpha = Mathf.Clamp(alpha, 0, 1);
                        alpha += Time.deltaTime * speeds;
                        blur.SetFloat("_Blur", alpha * 10);
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
                        blur.SetFloat("_Blur", alpha * 10);
                        canvas.alpha = alpha;
                    }
                    isActive = false;
                    break;
            }
        }
    }
}