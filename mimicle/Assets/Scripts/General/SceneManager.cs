using UnityEngine;
using Mimicle.Extend;

namespace Mimicle
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => Site.Load(Constant.Main);
        public void ToTitle() => Site.Load(Constant.Title);
    }
}
