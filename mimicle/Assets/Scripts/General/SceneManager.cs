using UnityEngine;
using MyGame.Utils;

namespace MyGame
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => MyScene.Load(Constant.Main);
        public void ToTitle() => MyScene.Load(Constant.Title);
    }
}
