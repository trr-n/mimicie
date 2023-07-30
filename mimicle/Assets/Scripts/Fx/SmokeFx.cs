using UnityEngine;
using Self.Utility;

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