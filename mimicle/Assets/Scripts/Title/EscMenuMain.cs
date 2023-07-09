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
        bool maxblur = false;
        (float fade, float _) speeds = (10, 1);

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            blur.SetFloat("_Blur", 0f);
        }

        /*TODO
        
        */
        void Update()
        {
            if (Mynput.Down(KeyCode.Escape))
            {
                if (!isActive)
                {
                    StartCoroutine(PanelAlpha());
                }
                else
                {

                }
            }
        }

        void Canvas()
        {

        }

        IEnumerator PanelAlpha()
        {
            while (canvas.alpha < 1)
            {
                yield return null;
                canvas.alpha = Mathf.Lerp(0, 1, Time.deltaTime * speeds.fade);
            }
            maxblur = true;
        }
    }
}
