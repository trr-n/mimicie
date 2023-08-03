using UnityEngine;
using Self.Utils;

namespace Self
{
    public class SmokeFx : MonoBehaviour
    {
        Stopwatch effectStopwatch = new(true);

        void Update()
        {
            if (effectStopwatch.sf >= 1.5f)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}