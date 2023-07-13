using UnityEngine;
using UnityEngine.UI;
using UnionEngine.Extend;

namespace UnionEngine
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
