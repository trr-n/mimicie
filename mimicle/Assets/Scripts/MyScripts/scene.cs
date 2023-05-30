using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mimic.Extend
{
    public class scene
    {
        public static void load(string name) => SceneManager.LoadScene(name);
        public static void load(int index) => SceneManager.LoadScene(index);

        public static string active()
        => SceneManager.GetActiveScene().name;
    }
}
