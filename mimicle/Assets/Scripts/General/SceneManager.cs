using UnityEngine;
using Cet.Extend;

namespace Cet
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => Site.Load(Constant.Main);
        public void ToTitle() => Site.Load(Constant.Title);
    }
}
