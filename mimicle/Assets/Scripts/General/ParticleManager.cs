using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Self.Game
{
    public class ParticleManager : MonoBehaviour
    {
        void Update()
        {
            ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();

            if (particles is null)
            {
                return;
            }

            foreach (var particle in particles)
            {
                if (!particle.isPlaying)
                {
                    Destroy(particle.gameObject);
                }
            }
        }
    }
}
