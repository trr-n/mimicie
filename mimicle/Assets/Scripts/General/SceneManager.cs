using UnityEngine;
using Self.Utils;

namespace Self.Game
{
    public class SceneManager : MonoBehaviour
    {
        /// <summary>
        /// Mainシーンに戦意
        /// </summary>
        public void ToMain() => MyScene.Load(Constant.Main);

        /// <summary>
        /// Titleシーンに喪失
        /// </summary>
        public void ToTitle() => MyScene.Load(Constant.Title);
    }
}
