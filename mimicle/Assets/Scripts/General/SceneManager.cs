using UnityEngine;
using Self.Utils;

namespace Self
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => MyScene.Load(Constant.Main);
        public void ToTitle() => MyScene.Load(Constant.Title);
    }
}
