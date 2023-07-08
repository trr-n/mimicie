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
        Image blur;
        [SerializeField]
        GameObject[] buttons;

        CanvasGroup canvas;
        bool isActive = false;
        bool isActive1 = false;

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        // TODO
        void Update()
        {
            isActive1 = canvas.alpha >= 1;
            if (Mynput.Down(KeyCode.Escape))
            {
                if (!isActive)
                {
                    StartCoroutine(Panels());
                }
                else
                {

                }
            }
        }

        IEnumerator Panels()
        {
            while (canvas.alpha <= 1)
            {
                yield return null;
            }
        }
    }
}
