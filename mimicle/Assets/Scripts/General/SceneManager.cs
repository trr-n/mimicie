using UnityEngine;
using Self.Utility;

namespace Self
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => MyScene.Load(Constant.Main);
        public void ToTitle() => MyScene.Load(Constant.Title);
    }
}
