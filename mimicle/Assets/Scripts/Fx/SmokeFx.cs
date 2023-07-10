using UnityEngine;
using Cet.Extend;

namespace Cet
{
    public class SmokeFx : MonoBehaviour
    {
        Stopwatch sw_fx = new(true);

        void Update()
        {
            if (sw_fx.sf >= 1.5f)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}