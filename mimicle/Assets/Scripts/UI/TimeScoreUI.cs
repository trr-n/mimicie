using UnityEngine;
using UnityEngine.UI;

namespace Mimical
{
    public class TimeScoreUI : MonoBehaviour
    {
        [SerializeField]
        Text score, time;

        void Update()
        {
            score.fontSize = time.fontSize = 20;
            score.text = $"Time: {Score.Now}";
            time.text = $"Score: {Score.Time}";
        }
    }
}
