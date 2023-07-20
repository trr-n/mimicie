using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyGame.Utils;

namespace MyGame
{
    public class DieMenu : MonoBehaviour
    {
        [SerializeField]
        BlurUI blur;

        [SerializeField]
        Text stateText, score, time;

        CanvasGroup canvas;
        float a = 0f;

        string[] StateText => new string[] { "Failure...", "You did it!" };

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }

        public void SetText(bool isDead)
        {
            stateText.text = isDead ? StateText[0] : StateText[1];
        }

        void Update()
        {
            this.score.text = "Score: " + Score.Now;
            this.time.text = "Time: " + Score.Time;
            // ブラーがマックスになったらスコア表示
            if (!blur.Max)
            {
                return;
            }

            a = Mathf.Clamp(a, 0, 1);
            a += Time.unscaledDeltaTime * 10;
            canvas.alpha = a;

            if (Numeric.Twins(1, a))
            {
                a = 1;
                // blur.Reblur();
            }
        }
    }
}