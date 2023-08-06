using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class SmokeFx : MonoBehaviour
    {
        readonly Stopwatch effectStopwatch = new(true);

        void Update()
        {
            if (effectStopwatch.sf >= 1.5f)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}