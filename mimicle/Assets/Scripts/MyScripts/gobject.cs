using UnityEngine;

namespace Mimical.Extend
{
    public static class gobject
    {
        public static GameObject ins(
            this GameObject[] gobjects, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(
            gobjects[random.ice(gobjects.Length)], position, rotation);

        public static GameObject ins(
            this GameObject gobject, Vector3 position, Quaternion rotation)
        => UnityEngine.MonoBehaviour.Instantiate(gobject, position, rotation);

        public static bool Exist(this GameObject gobject) => gobject;

        public static bool Compare(this Collision info, string tag)
        => info.gameObject.CompareTag(tag);

        public static bool Compare(this Collider info, string tag)
        => info.CompareTag(tag);

        public static GameObject Find(string tag)
        => GameObject.FindGameObjectWithTag(tag);

        public static GameObject[] Finds(string tag)
        => GameObject.FindGameObjectsWithTag(tag);
    }
}
