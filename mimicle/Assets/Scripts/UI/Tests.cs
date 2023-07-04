using UnityEngine;
using UnityEngine.UI;
using Mimical.Extend;

namespace Mimical
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
