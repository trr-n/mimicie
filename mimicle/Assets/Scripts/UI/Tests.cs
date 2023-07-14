using UnityEngine;
using UnityEngine.UI;
using Feather.Utils;

namespace Feather
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
