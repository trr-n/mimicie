using UnityEngine;
using UnityEngine.UI;
using Cet.Extend;

namespace Cet
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
