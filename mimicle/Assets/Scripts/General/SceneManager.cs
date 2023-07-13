using UnityEngine;
using UnionEngine.Extend;

namespace UnionEngine
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => Site.Load(Constant.Main);
        public void ToTitle() => Site.Load(Constant.Title);
    }
}
