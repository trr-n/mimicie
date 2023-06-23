using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace Mimical.Extend
{
    public class scene
    {
        public static void Load(string name) => LoadScene(name);
        public static void Load(int index) => LoadScene(index);
        public static void Load() => LoadScene(scene.Active());
        public static string Active() => GetActiveScene().name;
        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
    }
}
