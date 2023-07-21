using UnityEngine;
using MyGame.Utils;

namespace MyGame
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