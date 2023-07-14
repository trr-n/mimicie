using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;

namespace Feather.Test
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
                sw.Restart();
            t.text = sw.SpentF(SWFormat.second).ToString();
        }
    }
}
