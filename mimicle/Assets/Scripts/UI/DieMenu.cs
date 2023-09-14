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

        CanvasGroup cGroup;
        float alpha = 0f;

        string[] StateTexts => new[] {
            "Did it!",
            "Complete!",
            "Well done!",
            "GOAT!",
            "Special!",
            "Thanks for playing!"
        };

        void OnEnable()
        {
            stateT.SetText(StateTexts.Choice3());
            scoreT.SetText("Score: " + Score.CurrentScore);
            timeT.SetText("Time: " + Score.CurrentTime);
        }

        void Start()
        {
            cGroup = GetComponent<CanvasGroup>();
            cGroup.alpha = 0;
        }

        void Update()
        {
            // scoreT.SetText("Score: " + finals.Score);
            // timeT.SetText("Time: " + finals.Time);
            // stateT.color = Color.HSVToRGB(Time.unscaledTime * 5 % 1, 1, 1);
            stateT.color = Color.HSVToRGB(Time.unscaledTime * 5 % 1, 1, 1);

            if (!blur.IsDone) return;

            alpha = Mathf.Clamp01(alpha);

            alpha += Time.unscaledDeltaTime * 10;
            cGroup.alpha = alpha;

            if (Numeric.Twins(1, alpha))
            {
                alpha = 1;
            }
        }
    }
}