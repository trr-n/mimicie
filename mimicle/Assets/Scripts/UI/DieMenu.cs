using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Game
{
    public class DieMenu : MonoBehaviour
    {
        [SerializeField]
        BlurUI blur;

        [SerializeField]
        Text stateT, scoreT, timeT;

        CanvasGroup canvas;
        float alpha = 0f;

        (int Score, int Time) finals = (0, 0);

        readonly string[] StateTexts = { "Did it!", "Complete!", "Well done!", "YOUR ARE GOAT!" };

        void OnEnable()
        {
            finals.Score = Score.CurrentScore;
            finals.Time = Score.CurrentTime;

            stateT.text = StateTexts.Choice3();
        }

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }

        void Update()
        {
            scoreT.SetText("Score: " + finals.Score);
            timeT.SetText("Time: " + finals.Time);
            // stateT.color = Color.HSVToRGB(Time.unscaledTime * 5 % 1, 1, 1);
            stateT.SetColor(Color.HSVToRGB(Time.unscaledTime * 5 % 1, 1, 1));

            if (!blur.IsDone)
            {
                return;
            }

            alpha = Mathf.Clamp01(alpha);

            alpha += Time.unscaledDeltaTime * 10;
            canvas.alpha = alpha;

            if (Numeric.Twins(1, alpha))
            {
                alpha = 1;
            }
        }
    }
}