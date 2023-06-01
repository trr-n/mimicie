using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mimical.Extend;

namespace Mimical
{
    public class SceneTrans : MonoBehaviour
    {
        public static void sTitle() => scene.Load(cst.Main);
    }
}
