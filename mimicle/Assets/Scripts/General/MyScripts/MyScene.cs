using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace Self.Utils
{
    public class MyScene
    {
        public static void Load(string name) => LoadScene(name);
        public static void Load(int index) => LoadScene(index);
        public static void Load() => LoadScene(active);

        public static string active => GetActiveScene().name;

        public static AsyncOperation LoadAsync(string name) => LoadSceneAsync(name);
        public static AsyncOperation LoadAsync(int index) => LoadSceneAsync(index);
    }
}
