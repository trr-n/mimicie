using UnityEngine;

namespace Mimic.Extend
{
    public static class gobject
    {
        public static GameObject ins(
            this GameObject[] gobjects, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(
            gobjects[random.choice(gobjects.Length)], position, rotation);

        public static GameObject ins(
            this GameObject gobject, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(gobject, position, rotation);

        public static bool exist(this GameObject gobject) => gobject;

        public static bool compare(this Collision info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool compare(this Collider info, string tag)
        => info.CompareTag(tag);

        public static GameObject find(string tag)
        => GameObject.FindGameObjectWithTag(tag);

        public static GameObject[] finds(string tag)
        => GameObject.FindGameObjectsWithTag(tag);
    }
}
