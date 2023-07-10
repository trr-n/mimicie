using UnityEngine;
using UnityEngine.UI;

namespace Cet
{
    public class TimeScoreUI : MonoBehaviour
    {
        [SerializeField]
        Text score, time;

        void Update()
        {
            score.fontSize = time.fontSize = 20;
            score.text = $"Time: {Score.Time}";
            time.text = $"Score: {Score.Now}";
        }
    }
}
