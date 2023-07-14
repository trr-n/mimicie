using UnityEngine;
using Feather.Utils;

namespace Feather
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => Parallel.Load(Constant.Main);
        public void ToTitle() => Parallel.Load(Constant.Title);
    }
}
