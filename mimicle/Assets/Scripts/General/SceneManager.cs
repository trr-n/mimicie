using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SceneManager : MonoBehaviour
    {
        public void sMain() => Section.Load(Constant.Main);
        public void sTitle() => Section.Load(Constant.Title);
    }
}
