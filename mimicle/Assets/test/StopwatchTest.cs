using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self.Test
{
    public class StopwatchTest : MonoBehaviour
    {
        [SerializeField] Text t;
        Stopwatch sw;

        void Start()
        {
            sw = new();
            sw.Start();
        }

        void Update()
        {
            if (sw.Second() > 5)
            {
                sw.Restart();
            }

            t.text = sw.SpentF(StopwatchFormat.second).ToString();
        }
    }
}
