using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mimical.Extend
{
    public class scene
    {
        public static void Load(string name) => SceneManager.LoadScene(name);

        public static void Load(int index) => SceneManager.LoadScene(index);

        public static void Load() => SceneManager.LoadScene(scene.Active());

        public static string Active() => SceneManager.GetActiveScene().name;

    }
}
