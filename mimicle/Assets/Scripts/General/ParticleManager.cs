using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mimicle
{
    public class ParticleManager : MonoBehaviour
    {
        void Update()
        {
            var particles = GameObject.FindObjectsOfType<ParticleSystem>();
            if (particles is null)
            {
                return;
            }
            foreach (var i in particles)
            {
                if (!i.isPlaying)
                {
                    Destroy(i.gameObject);
                }
            }
        }
    }
}
