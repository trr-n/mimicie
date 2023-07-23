using UnityEngine;
using UnityEngine.UI;
using Self.Utils;

namespace Self
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text fpsT;

        void Update()
        {
            fpsT.text = Render.FPS().ToString();
        }
    }
}
