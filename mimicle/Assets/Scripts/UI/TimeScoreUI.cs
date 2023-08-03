using UnityEngine;
using UnityEngine.UI;

namespace Self
{
    public class TimeScoreUI : MonoBehaviour
    {
        [SerializeField]
        Text score, time;

        void Update()
        {
            score.fontSize = time.fontSize = 20;
            score.text = $"Time: {Score.CurrentTime}";
            time.text = $"Score: {Score.CurrentScore}";
        }
    }
}
