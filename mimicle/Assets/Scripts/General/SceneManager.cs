using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SceneManager : MonoBehaviour
    {
        public void ToMain() => Section.Load(Constant.Main);
        public void ToTitle() => Section.Load(Constant.Title);
    }
}
