using UnityEngine;
using UnityEngine.UI;
using MyGame.Utils;

namespace MyGame
{
    public class Tests : MonoBehaviour
    {
        [SerializeField]
        Text fpsT;

        void Update()
        {
            fpsT.text = Render.SetFPS().ToString();
        }
    }
}
