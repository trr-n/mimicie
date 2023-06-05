using System.Collections;
using System.Collections.Generic;

namespace Mimical.Extend
{
    public class scene
    {
        public static void Load(string name)
        => UnityEngine.SceneManagement.SceneManager.LoadScene(name);

        public static void Load(int index)
        => UnityEngine.SceneManagement.SceneManager.LoadScene(index);

        public static void Load()
        => UnityEngine.SceneManagement.SceneManager.LoadScene(scene.Active());


        public static string Active()
        => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
}
