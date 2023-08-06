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

        (int Score, int Time) finals;

        void OnEnable()
        {
            finals.Score = Score.CurrentScore;
            finals.Time = Score.CurrentTime;
        }

        void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;

            stateT.text = "Dit it !";
        }

        void Update()
        {
            if (scoreT == null)
            {
                return;
            }

            scoreT.text = "Score: " + finals.Score;
            timeT.text = "Time: " + finals.Time;

            // ブラーがマックスになったらスコア表示
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