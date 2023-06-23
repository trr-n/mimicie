using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mimical.Extend;

namespace Mimical
{
    public class SceneManager : MonoBehaviour
    {
        public void sMain() => scene.Load(Constant.Main);
        public void sTitle() => scene.Load(Constant.Title);
    }
}
