using UnityEngine;
using UnityEngine.UI;
using Mimicle.Extend;

namespace Mimicle
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text fpsT;

        void Update()
        {
            fpsT.text = Render.Fps().ToString();
        }
    }
}
