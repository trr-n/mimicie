using UnityEngine;
using Feather.Utils;

namespace Feather
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => MyScene.Load(Constant.Main);
        public void ToTitle() => MyScene.Load(Constant.Title);
    }
}
